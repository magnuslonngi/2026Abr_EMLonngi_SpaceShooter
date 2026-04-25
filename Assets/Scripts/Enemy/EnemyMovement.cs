using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private Vector2 _direction;
    
    private Rigidbody2D _rigidbody2D;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _rigidbody2D.linearVelocity = _speed * _direction.normalized;
    }

}
