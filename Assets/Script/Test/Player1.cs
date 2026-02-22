using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player1 : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private float _moveSpeed;
    private PlayerInput _playerInput;
    private InputAction _move;
    private Vector2 _horizontal;
    private Vector3 _playerPostion;

    [Header("FPS Camera Setings")]
    [SerializeField] private float _mouseSensitivity = 2f;
    [SerializeField] private Transform _cameraTransform;

    [SerializeField] private Gun _gun;//銃仲介

    private InputAction _lookAction;
    private float _yaw; //左右視点のこと　Y軸＝Yaw
    private Vector2 _lookInput;
    private float _verticalRotation;

    private Transform _tr;
    private Rigidbody _rb;

    [SerializeField] private GameObject _effect;//銃のマズルフラッシュ,エフェクトの奴後々場所を帰る

    private const string _fire = "Fire";
    private const string _llok = "Look";
    private const string _1 = "1";
    private const string _2 = "2";
    private const string _3 = "3";
    private const string _4 = "4";

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody>();
        _tr = GetComponent<Transform>();
        _move = _playerInput.actions["Move"];
        _lookAction = _playerInput.actions["look"];
    }

    // Update is called once per frame
    void Update()
    {
        //======== カメラコントロール =========
        _lookInput = _lookAction.ReadValue<Vector2>();

        _yaw += _lookInput.x * _mouseSensitivity;
        _verticalRotation -= _lookInput.y * _mouseSensitivity;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -80f, 80f);

        _tr.rotation = Quaternion.Euler(0f, _yaw, 0f); // プレイヤー本体は左右
        _cameraTransform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f); // カメラは上下

        if (_playerInput.actions["Fire"].WasPressedThisFrame())
        {
            _gun.Fire();
            _effect.SetActive(false);//これらの場所も変える
            _effect.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        //======== 移動 =========
        if (_playerInput.actions["Move"].IsPressed())
        {
            _horizontal = _move.ReadValue<Vector2>();
            _playerPostion = new Vector3(_horizontal.x, 0, _horizontal.y).normalized;
            _rb.MovePosition(_tr.position + _playerPostion * _moveSpeed * Time.fixedDeltaTime);
            //トランスフォームトランスレートのほうがいい？
        }

        // === 視点回転 === 

        _yaw += _lookInput.x * _mouseSensitivity;
        _verticalRotation -= _lookInput.y * _mouseSensitivity;// 上下回転を入力に応じて増減
        _verticalRotation = Mathf.Clamp(_verticalRotation, -80f, 80f); // 上下視点の角度制限

        transform.rotation = Quaternion.Euler(0f, _yaw, 0f); // プレイヤー本体は左右
        _cameraTransform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f); // カメラは上下
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
    }
}
