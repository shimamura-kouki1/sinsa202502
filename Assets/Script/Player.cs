using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private float _WalkSpeed;//歩き速度
    [SerializeField] private float _sprintSpeed;//走る速度
    private float _moveSpeed;//動く速度
    private bool _isSprinting = false;//歩きと走りの判定

    private CharacterController _characterController;
    private Vector2 _moveInput;//移動入力

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

        _moveSpeed = _isSprinting ? _WalkSpeed : _sprintSpeed;//歩きと走りを判別
        _characterController.Move((transform.right * _moveInput.x + transform.forward * _moveInput.y) * _WalkSpeed * Time.deltaTime);//最終的な移動スピード
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
        if (context.performed) _isSprinting = true;//押されたらスプリント
        if (context.canceled) _isSprinting = false;//離れたら歩く
    }
}
