using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private TurretSelector _turretSelector;

    private PlayerInput _playerInput;
    private InputAction _fire;
    private InputAction _turretSwitch;
    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        _fire = _playerInput.actions["Fire"];
        _turretSwitch = _playerInput.actions["Switch"];
    }
    private void OnEnable()
    {
        _fire.performed += OnFire;
        _turretSwitch.performed += OnSwitch;
    }
    private void OnDestroy()
    {
        _fire.performed -= OnFire;
        _turretSwitch.performed -= OnSwitch;
    }

    private void Update()
    {
        HandleAim();
    }

    private void HandleAim()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPos = _camera.ScreenToWorldPoint(mousePos);
        worldPos.z = 0f;

    }
    public void OnFire(InputAction.CallbackContext ctx)
    {
        Vector3 origin = _camera.transform.position;
        Vector3 direction = _camera.transform.forward;

        _turretSelector.CurrentTurret.Fire(origin, direction);
    }

    public void OnSwitch(InputAction.CallbackContext ctx)
    {
        int index = (int)ctx.ReadValue<float>() -1; //intに変換して受け取る

        _turretSelector.Select(index);
    }
}
