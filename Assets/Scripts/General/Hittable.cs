using UnityEngine;

public class Hittable : MonoBehaviour
{
    [SerializeField] public int HealthPoints = 100;
    [SerializeField] private int _onDestroyScore = 10;
    [SerializeField] private bool _destroyOnCollision;
    [SerializeField] private int _collisionDamage = 10;
    [SerializeField] private bool _isPlayer;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private int _powerUpSpawnPercentage = 20;
    [SerializeField] private GameObject[] _powerUpsPrefabs;

    private GameManager _gameManager;
    private int _starterHealth;

    private void Awake()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
    }

    private void Start()
    {
        _starterHealth = HealthPoints;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!_destroyOnCollision) return;

        if (other.gameObject.TryGetComponent<Hittable>(out var hittable))
        {
            hittable.Hit(_collisionDamage);
        }

        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        SpawnPowerUpOnDeath();
        Destroy(gameObject);
    }

    public void Hit(int damage)
    {
        HealthPoints -= damage;
        if (_isPlayer) _gameManager.UpdateHealth(HealthPoints);

        if (HealthPoints > 0) return;

        HealthPoints = 0;
        _gameManager.UpdateScore(_onDestroyScore);

        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        SpawnPowerUpOnDeath();
        Destroy(gameObject);
    }

    public void IncreaseHealth(int health)
    {
        if (!_isPlayer) return;

        HealthPoints += health;
        if (HealthPoints > _starterHealth) HealthPoints = _starterHealth;

        _gameManager.UpdateHealth(HealthPoints);
    }

    private void SpawnPowerUpOnDeath()
    {
        if (_powerUpsPrefabs.Length <= 0 || _isPlayer) return;

        bool shouldSpawn = Random.Range(1, 100) <= _powerUpSpawnPercentage ? true : false;

        if (!shouldSpawn) return;

        int randomIndex = Random.Range(0, _powerUpsPrefabs.Length);
        Instantiate(_powerUpsPrefabs[randomIndex], transform.position, Quaternion.identity);
    }
}