using System;
using UnityEngine;

public class Hittable : MonoBehaviour
{
    [SerializeField] public int HealthPoints = 100;
    [SerializeField] private int _onDestroyScore = 10;
    [SerializeField] private bool _destroyOnCollision;
    [SerializeField] private int _collisionDamage = 10;
    [SerializeField] private bool _isPlayer;
    [SerializeField] private GameObject _explosionPrefab;

    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (!_destroyOnCollision) return;

        if (other.gameObject.TryGetComponent<Hittable>(out var hittable))
        {
            hittable.Hit(_collisionDamage);
        }

        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
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
        Destroy(gameObject);
    }
}