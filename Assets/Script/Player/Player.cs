using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private float _WalkSpeed;//�������x
    [SerializeField] private float _sprintSpeed;//���鑬�x
    private float _moveSpeed;//�������x
    private bool _isSprinting = false;//�����Ƒ���̔���

    [Header("Look Settings")]
    [SerializeField] private float _mouseSensitivity = 2f;//�}�E�X���x
    [SerializeField] private Transform _cameraTransform; //���_�J����

    private CharacterController _characterController;
    private Vector2 _moveInput;//�ړ�����
    private Vector2 _lookInput;//���_����
    private float _verticalRotation;//�㉺�̎��_�@��]�̊p�x
    private float _yaw;              // ���E���_�̊p�x


    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        //if (_inputAction.actions["move"].IsPressed())

        _moveSpeed = _isSprinting ? _WalkSpeed : _sprintSpeed;//�����Ƒ���𔻕�
        _characterController.Move((transform.right * _moveInput.x + transform.forward * _moveInput.y) * _WalkSpeed * Time.deltaTime);//�ŏI�I�Ȉړ��X�s�[�h

        _yaw += _lookInput.x * _mouseSensitivity;
        _verticalRotation -= _lookInput.y * _mouseSensitivity;// �㉺��]����͂ɉ����đ���
        _verticalRotation = Mathf.Clamp(_verticalRotation, -80f, 80f); // �㉺���_�̊p�x����

        transform.rotation = Quaternion.Euler(0f, _yaw, 0f); // �v���C���[�{�͍̂��E
        _cameraTransform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f); // �J�����͏㉺
    }

    public void Onmove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        Debug.Log("aaa");
    }
    public void OnActoin(InputAction.CallbackContext context)
    {

    }
    public void Onsprint(InputAction.CallbackContext context)
    {
        if (context.performed) _isSprinting = true;//�����ꂽ��X�v�����g
        if (context.canceled) _isSprinting = false;//���ꂽ�����
    }
}
