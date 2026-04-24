using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 10;
    [SerializeField] private Vector2 _direction = new Vector2(1, 0);
    [SerializeField] private int _damage = 5;
    [SerializeField][Range(1, 10)] private int _collisionQuantity = 1;

    private int _collisionCount = 0;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rigidbody2D.linearVelocity = _speed * _direction.normalized;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Hittable>(out var hittable))
        {
            hittable.Hit(_damage);

            if (!(_collisionQuantity >= _collisionCount)) return;

            Destroy(gameObject);
        }
    }
}
