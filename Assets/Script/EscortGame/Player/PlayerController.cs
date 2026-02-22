using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private TurretSelector _turretSelector;

    [Header("Look")]
    [SerializeField] private float _mouseSensitivity = 50f;
    [SerializeField] private float _limitX = 60f;
    [SerializeField] private float _limitY = 60f; // 左右制限
    private float _baseYRotation;   // 砲台の正面
    private float _currentOffsetY;  // 正面からのズレ
    private float _xRotation = 0f;


    private const string _fire = "Fire";
    private const string _switch = "Switch";
    private const string _1 = "1";
    private const string _2 = "2";
    private const string _3 = "3";
    private const string _4 = "4";

    private PlayerInput _playerInput;
    private InputAction _fireAction;
    private InputAction _turretSwitch;
    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        _fireAction = _playerInput.actions[_fire];
        _turretSwitch = _playerInput.actions[_switch];
    }
    private void OnEnable()
    {
        _fireAction.performed += OnFire;
        _turretSwitch.performed += OnSwitch;
        _turretSelector.OnTurretChanged += SetBaseRotation;
    }
    private void OnDestroy()
    {
        _fireAction.performed -= OnFire;
        _turretSwitch.performed -= OnSwitch;
        _turretSelector.OnTurretChanged -= SetBaseRotation;
    }

    private void Update()
    {
        HandleLook();
    }

    private void HandleLook()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue() * _mouseSensitivity * Time.deltaTime;

        //上下
        _xRotation -= mouseDelta.y;
        _xRotation = Mathf.Clamp(_xRotation,-_limitX, _limitX);

        //左右
        _currentOffsetY += mouseDelta.x;
        _currentOffsetY = Mathf.Clamp(_currentOffsetY, -_limitY, _limitY);

        float finalY = _baseYRotation + _currentOffsetY;

        transform.rotation = Quaternion.Euler(0f, finalY, 0f);
        _camera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        //カメラのX,Y両方制限したらガクガクし始めた
    }

    public void SetBaseRotation(Transform turret)
    {
        _baseYRotation = turret.eulerAngles.y;

        _currentOffsetY = 0f;

        transform.rotation = Quaternion.Euler(0f, _baseYRotation, 0f);
    }
    public void OnFire(InputAction.CallbackContext ctx)
    {
        Debug.Log("Fire");
        Vector3 origin = _camera.transform.position;
        Vector3 direction = _camera.transform.forward;

        _turretSelector.CurrentTurret.Fire(origin, direction);
    }

    public void OnSwitch(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        var control = ctx.control;
        if (control.name == _1)
            _turretSelector.Select(0);
        else if (control.name == _2)
            _turretSelector.Select(1);
        else if (control.name == _3)
            _turretSelector.Select(2);
        else if (control.name == _4)
            _turretSelector.Select(3);
    }
}
