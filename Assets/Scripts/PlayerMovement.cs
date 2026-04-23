using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference _moveInput;
    [SerializeField] private float _speed = 5f;

    private Rigidbody2D _rigidbody2D;
    private Vector2 _moveDirection;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _moveInput.action.performed += OnMove;
        _moveInput.action.canceled += OnMove;
    }
    
    private void OnEnable()
    {
        _moveInput.action.Enable();
    }

    private void Update()
    {
        _rigidbody2D.linearVelocity = _moveDirection * _speed;
    }

    private void OnDisable()
    {
        _moveInput.action.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _moveDirection = context.ReadValue<Vector2>();
    }
}
