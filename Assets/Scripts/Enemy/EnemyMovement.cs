using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private Vector2 _direction;
    [SerializeField] private bool _destroyOnCollision;
    [SerializeField] private int _collisionDamage = 10;
    
    private Rigidbody2D _rigidbody2D;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _rigidbody2D.linearVelocity = _speed * _direction.normalized;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!_destroyOnCollision) return;

        if (other.gameObject.TryGetComponent<Hittable>(out var hittable))
        {
            hittable.HealthPoints -= _collisionDamage;
        }

        Destroy(gameObject);
    }
}
