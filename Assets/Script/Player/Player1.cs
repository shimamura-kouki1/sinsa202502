using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Player1 : MonoBehaviour
{
    [Header("Player")]
    [SerializeField]private float _moveSpeed;
    private PlayerInput _playerInput;
    private InputAction _move;
    private Vector2 _horizontal;
    private Vector2 _vertical;
    private Vector3 _playerPostion;

    private Transform _tr;
    private Rigidbody _rb;


    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody>();
        _tr = GetComponent<Transform>();
        _move = _playerInput.actions["Move"];
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
       //======== ˆÚ“® =========
        if (_playerInput.actions["Move"].IsPressed())
        {
            Debug.Log("aa");
            _horizontal = _move.ReadValue<Vector2>();
            _playerPostion = new Vector3 (_horizontal.x, 0, _horizontal.y);
            _rb.MovePosition(_tr.position + _playerPostion * _moveSpeed * Time.fixedDeltaTime);
        }
    }
}
