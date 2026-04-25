using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference _moveInput;
    [SerializeField] private float _speed = 5f;

    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _downSprite;
    [SerializeField] private Sprite _upSprite;

    private Rigidbody2D _rigidbody2D;
    private Vector2 _moveDirection;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

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

        if (_moveDirection.y > 0)
        {
            _spriteRenderer.sprite = _upSprite;
        } 
        else if (_moveDirection.y < 0)
        {
            _spriteRenderer.sprite = _downSprite;
        }
        else
        {
            _spriteRenderer.sprite = _defaultSprite;
        }

    }
}
