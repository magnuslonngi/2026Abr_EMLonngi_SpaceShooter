using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private Vector2 _direction;
    
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rigidbody2D.linearVelocity = _speed * _direction.normalized;
    }

    public Vector2 GetCurrentDirection()
    {
        return _direction;
    }

    public void FlipHorizontalDirection()
    {
        _direction.x *= -1;
        _rigidbody2D.linearVelocity = _speed * _direction.normalized;
    }

    public void FlipVerticalDirection()
    {
        _direction.y *= -1;
        _rigidbody2D.linearVelocity = _speed * _direction.normalized;
    }

    public void StopMoving()
    {
        _rigidbody2D.linearVelocity = Vector2.zero;
    }

    public void StopHorizontalMovement()
    {
        _direction.x = 0;
        _rigidbody2D.linearVelocity = _speed * _direction.normalized;
    }

    public void StopVerticalMovement()
    {
        _direction.y = 0;
        _rigidbody2D.linearVelocity = _speed * _direction.normalized;
    }
}
