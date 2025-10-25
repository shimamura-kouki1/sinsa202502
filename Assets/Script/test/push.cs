using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))] // ��Ƃ��� CharacterController ���g�����ړ�
public class PushAligner : MonoBehaviour
{
    [Header("Input (assign from Input Actions)")]
    [Tooltip("�����{�^���� InputAction �� Inspector �Ŋ��蓖�Ă�i��: Player/Push -> 'E'�j")]
    public InputActionReference pushActionRef;

    [Header("Detection")]
    [SerializeField] private float rayDistance = 1.8f;          // �Ƌ�o����
    [SerializeField] private LayerMask pushableLayer;          // ������Ƌ�C���[

    [Header("Align (player positioning)")]
    [SerializeField] private float alignDistance = 0.6f;       // �Ƌ�̑O�ɗ�����
    [SerializeField] private float alignMoveSpeed = 6f;        // �ʒu���킹�̈ړ����x�i�����قǑ����Ɉړ��j
    [SerializeField] private float alignRotateSpeed = 720f;    // �������킹�̉�]���x�ideg/sec�j

    [Header("Pushing")]
    [SerializeField] private float pushForce = 200f;           // �����́iRigidbody �ɉ�����́j
    [SerializeField] private bool useContinuousForce = true;   // �z�[���h���ɗ͂����������邩

    // �������
    private CharacterController _cc;
    private bool _isAligning = false;
    private bool _isPushing = false;
    private Rigidbody _targetRb;
    private Transform _targetTransform;
    private Vector3 _alignTargetPos;
    private Quaternion _alignTargetRot;

    private void Awake()
    {
        _cc = GetComponent<CharacterController>();
        if (pushActionRef == null) Debug.LogWarning("pushActionRef �������蓖�Ăł��BInspector �Ŋ��蓖�ĂĂ��������B");
    }

    private void OnEnable()
    {
        if (pushActionRef != null)
        {
            pushActionRef.action.started += OnPushStarted;
            pushActionRef.action.performed += OnPushPerformed;   // �z�[���h���̃C�x���g�i�K�v�Ȃ�j
            pushActionRef.action.canceled += OnPushCanceled;
            pushActionRef.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (pushActionRef != null)
        {
            pushActionRef.action.started -= OnPushStarted;
            pushActionRef.action.performed -= OnPushPerformed;
            pushActionRef.action.canceled -= OnPushCanceled;
            pushActionRef.action.Disable();
        }
    }

    // �����{�^�����������u��
    private void OnPushStarted(InputAction.CallbackContext ctx)
    {
        if (_isAligning || _isPushing) return; // ���ɏ������Ȃ疳��

        // �O���Ƀ��C�L���X�g���ĉƋ��T���i�v���C���[�̖ڐ��^���ʒu����j
        Vector3 origin = transform.position + Vector3.up * 0.8f; // �������₷��
        if (Physics.Raycast(origin, transform.forward, out RaycastHit hit, rayDistance, pushableLayer, QueryTriggerInteraction.Ignore))
        {
            _targetTransform = hit.collider.transform;
            _targetRb = hit.rigidbody;  // �� �C���ς�
            if (_targetRb == null)
            {
                Debug.Log("�������Ƃ����I�u�W�F�N�g�� Rigidbody ���A�^�b�`����Ă��܂���B");
                _targetTransform = null;
                return;
            }

            // ���ׂ��ʒu�ƌ������v�Z
            Vector3 directionToObject = (_targetTransform.position - transform.position);
            directionToObject.y = 0f;
            if (directionToObject.sqrMagnitude < 0.0001f)
            {
                directionToObject = transform.forward; // �߂������獡�̑O��
            }
            Vector3 objectForward = -directionToObject.normalized; // �I�u�W�F�N�g�Ɋ�����������
            _alignTargetPos = _targetTransform.position + objectForward * alignDistance;
            _alignTargetPos.y = transform.position.y; // ���������ɂ���i�K�v�Ȃ璲���j
            _alignTargetRot = Quaternion.LookRotation((_targetTransform.position - _alignTargetPos).normalized, Vector3.up);

            // �J�n
            _isAligning = true;
            StartCoroutine(AlignThenStartPushing());
        }
    }

    // performed �̓z�[���h���ɌĂ΂�邱�Ƃ�����i�����ł͓��Ɏg��Ȃ����c���Ă���j
    private void OnPushPerformed(InputAction.CallbackContext ctx)
    {
        // �������Ȃ��i����ɌŒ�X�V�ŉ��͂�^����j
    }

    // �����{�^���𗣂����Ƃ�
    private void OnPushCanceled(InputAction.CallbackContext ctx)
    {
        // �����̂��~�߂�
        _isPushing = false;
        _isAligning = false;
        _targetRb = null;
        _targetTransform = null;
    }

    // �ʒu���킹���Ă��牟����Ԃ�
    private IEnumerator AlignThenStartPushing()
    {
        // �ʒu���킹�t�F�[�Y�i���炩�Ɉړ��{��]�j
        float timeout = 0.5f; // �������[�v������邽�߂̊ȒP�ȃ^�C���A�E�g�i�K�v�Ȃ璲���j
        float timer = 0f;

        while (timer < timeout)
        {
            // �ʒu�� CharacterController ���g���Ċ��炩�Ɉړ��i�Փ˂� CharacterController �ɔC����j
            Vector3 toTarget = _alignTargetPos - transform.position;
            Vector3 move = Vector3.zero;
            if (toTarget.magnitude > 0.01f)
            {
                Vector3 desiredMove = toTarget.normalized * alignMoveSpeed * Time.deltaTime;
                if (desiredMove.magnitude > toTarget.magnitude) desiredMove = toTarget;
                move = desiredMove;
                _cc.Move(move);
            }

            // ��]
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _alignTargetRot, alignRotateSpeed * Time.deltaTime);

            // �߂Â�����I��
            if (toTarget.magnitude <= 0.05f && Quaternion.Angle(transform.rotation, _alignTargetRot) <= 2f)
            {
                break;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        // �ʒu���킹���� �� ������ԊJ�n�i�z�[���h���Ă�����艟���j
        _isAligning = false;
        _isPushing = true;
    }

    private void FixedUpdate()
    {
        if (_isPushing && _targetRb != null)
        {
            // �v���C���[�������Ă�������i���������̂݁j
            Vector3 pushDir = transform.forward;
            pushDir.y = 0f;
            pushDir.Normalize();

            if (useContinuousForce)
            {
                // �����͂�������i�g�����̂��� ForceMode ��I�ׂ邪�����ł� Force�j
                _targetRb.AddForce(pushDir * pushForce * Time.fixedDeltaTime, ForceMode.Acceleration);
            }
            else
            {
                // 1�񂾂��̃C���p���X�ɂ������ꍇ�͕ʃ��W�b�N�ɂ���
                _targetRb.AddForce(pushDir * pushForce, ForceMode.Impulse);
                _isPushing = false; // ��x��������I���i�p�r�ɂ���ĕς��Ă��������j
            }
        }
    }

    // �f�o�b�O�p�F���o�I�Ɍ��₷������
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 origin = transform.position + Vector3.up * 0.8f;
        Gizmos.DrawLine(origin, origin + transform.forward * rayDistance);
        if (_targetTransform != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(_alignTargetPos, 0.05f);
            Gizmos.DrawLine(transform.position, _alignTargetPos);
        }
    }
}
