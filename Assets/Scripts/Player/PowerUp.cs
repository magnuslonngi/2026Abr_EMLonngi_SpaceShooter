using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _direction;
    [SerializeField] private PowerUpTypes _powerUpType;
    [SerializeField] private float _speedBoostDuration;
    [SerializeField] private float _speedBoostIncrease;
    [SerializeField] private float _fireRateDuration;
    [SerializeField] private float _fireRateIncrese;
    [SerializeField] private int _healthIncrease;

    private Rigidbody2D _rigidBody2D;

    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rigidBody2D.linearVelocity = _speed * _direction.normalized;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (_powerUpType)
        {
            case PowerUpTypes.SpeedBoost:
                if (other.gameObject.TryGetComponent<PlayerMovement>(out var playerMovement))
                {
                    playerMovement.StartCoroutine(playerMovement.TemporalIncreaseSpeed(_speedBoostDuration, _speedBoostIncrease));
                    Destroy(gameObject);
                }
            break;
            case PowerUpTypes.FireRate:
                if (other.gameObject.TryGetComponent<PlayerShoot>(out var playerShoot))
                {
                    playerShoot.StartCoroutine(playerShoot.IncreaseFireRate(_fireRateDuration, _fireRateIncrese));
                    Destroy(gameObject);
                }
            break;
            case PowerUpTypes.IncreaseHealth:
                if (other.gameObject.TryGetComponent<Hittable>(out var hittable))
                {
                    hittable.IncreaseHealth(_healthIncrease);
                    Destroy(gameObject);
                }
            break;
        }
    }
    
    private enum PowerUpTypes
    {
        SpeedBoost,
        FireRate,
        IncreaseHealth
    }
}