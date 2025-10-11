using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private float _WalkSpeed;//�������x
    [SerializeField] private float _sprintSpeed;//���鑬�x
    private float _moveSpeed;//�������x
    private bool _isSprinting = false;//�����Ƒ���̔���

    private CharacterController _characterController;
    private Vector2 _moveInput;//�ړ�����

    private InputAction _inputAction;

    // Start is called before the first frame update
    void Start()
    {
        _inputAction = GetComponent<InputAction>();
        _characterController = GetComponent<CharacterController>();
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        //if (_inputAction.actions["move"].IsPressed())

        _moveSpeed = _isSprinting ? _WalkSpeed : _sprintSpeed;//�����Ƒ���𔻕�
        _characterController.Move((transform.right * _moveInput.x + transform.forward * _moveInput.y) * _WalkSpeed * Time.deltaTime);//�ŏI�I�Ȉړ��X�s�[�h
    }

    public void Onmove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
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
