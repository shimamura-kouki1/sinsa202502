using UnityEngine;
using UnityEngine.InputSystem;

public class look : MonoBehaviour
{
    [Header("FPS Camera Setings")]
    [SerializeField] private float _mouseSensitivity = 2f;
    [SerializeField] private Transform _cameraTransform;

    private PlayerInput _playerInput;
    private InputAction _look;
    private float _yaw; //���E���_�̂��Ɓ@Y����Yaw
    private Vector2 _lookInput;
    private float _verticalRotation;

    private Transform _tr;
    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _tr = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        _lookInput = _look.ReadValue<Vector2>();

        _yaw += _lookInput.x * _mouseSensitivity;
        _verticalRotation -= _lookInput.y * _mouseSensitivity;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -80f, 80f);

        _tr.rotation = Quaternion.Euler(0f, _yaw, 0f); // �v���C���[�{�͍̂��E
        _cameraTransform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f); // �J�����͏㉺
    }
}
