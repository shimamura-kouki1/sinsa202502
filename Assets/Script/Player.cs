using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private float _WalkSpeed;//歩き速度
    [SerializeField] private float _sprintSpeed;//走る速度
    private float _moveSpeed;//動く速度
    private bool _isSprinting = false;//歩きと走りの判定

    [Header("Look Settings")]
    [SerializeField] private float _mouseSensitivity = 2f;//マウス感度
    [SerializeField] private Transform _cameraTransform; //視点カメラ

    private CharacterController _characterController;
    private Vector2 _moveInput;//移動入力
    private Vector2 _lookInput;//視点入力
    private float _verticalRotation;//上下の視点　回転の角度
    private float _yaw;              // 左右視点の角度


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

        _moveSpeed = _isSprinting ? _WalkSpeed : _sprintSpeed;//歩きと走りを判別
        _characterController.Move((transform.right * _moveInput.x + transform.forward * _moveInput.y) * _WalkSpeed * Time.deltaTime);//最終的な移動スピード

        _yaw += _lookInput.x * _mouseSensitivity;
        _verticalRotation -= _lookInput.y * _mouseSensitivity;// 上下回転を入力に応じて増減
        _verticalRotation = Mathf.Clamp(_verticalRotation, -80f, 80f); // 上下視点の角度制限

        transform.rotation = Quaternion.Euler(0f, _yaw, 0f); // プレイヤー本体は左右
        _cameraTransform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f); // カメラは上下
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
        if (context.performed) _isSprinting = true;//押されたらスプリント
        if (context.canceled) _isSprinting = false;//離れたら歩く
    }
}
