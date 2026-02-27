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

    [SerializeField] private int _startTurretIndex;//最初の砲台
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

    private void Start()
    {
        _turretSelector.Select(_startTurretIndex);//初期砲台を設定

        Cursor.visible = false;//マウス非表示

        Cursor.lockState = CursorLockMode.Locked;//カーソルを中央に固定
        
    }

    private void Update()
    {
        HandleLook();
    }

    /// <summary>
    /// マウス入力で視点移動
    /// </summary>
    private void HandleLook()
    {
        //マウスの移動量の取得
        Vector2 mouseDelta = Mouse.current.delta.ReadValue() * _mouseSensitivity * Time.deltaTime;

        //上下
        _xRotation -= mouseDelta.y;
        _xRotation = Mathf.Clamp(_xRotation,-_limitX, _limitX);

        //左右
        _currentOffsetY += mouseDelta.x;
        _currentOffsetY = Mathf.Clamp(_currentOffsetY, -_limitY, _limitY);

        float finalY = _baseYRotation + _currentOffsetY;

        //プレイヤー本体の左右回転
        transform.rotation = Quaternion.Euler(0f, finalY, 0f);

        //カメラの上下回転
        _camera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        //カメラのX,Y両方制限したらガクガクし始めた
    }

    /// <summary>
    /// 砲台変更時の角度を修正
    /// </summary>
    /// <param name="turret"></param>
    public void SetBaseRotation(Transform turret)
    {
        //砲台の向いてる方向を基準に
        _baseYRotation = turret.eulerAngles.y;

        _currentOffsetY = 0f;

        //プレイヤーの向きを更新
        transform.rotation = Quaternion.Euler(0f, _baseYRotation, 0f);
    }

    /// <summary>
    /// 発射時に呼ばれる
    /// </summary>
    /// <param name="ctx"></param>
    public void OnFire(InputAction.CallbackContext ctx)
    {
        Vector3 origin = _camera.transform.position;
        Vector3 direction = _camera.transform.forward;

        _turretSelector.CurrentTurret.Fire(origin, direction);
    }

    /// <summary>
    /// 砲台の切り替え
    /// </summary>
    /// <param name="ctx"></param>
    public void OnSwitch(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        var control = ctx.control;

        //入力されたキーに応じて砲台選択
        switch(control.name)
        {
            case _1:
                _turretSelector.Select(0);
                break;
            case _2:
                _turretSelector.Select(1);
                break;
            case _3:
                _turretSelector.Select(2);
                break;
            case _4:
                _turretSelector.Select(3);
                break;
            default:
                break;
        }
    }
}