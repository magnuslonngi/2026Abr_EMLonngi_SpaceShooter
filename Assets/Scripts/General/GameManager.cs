using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private RectTransform _playerHealthBar;
    private int _gameScore = 0;

    private float _startingHealth;
    private float _currentHealth;
    private float _healthBarStartingWidth;

    private void Awake()
    {
        _currentHealth = _startingHealth = _player.GetComponent<Hittable>().HealthPoints;
        _healthBarStartingWidth = _playerHealthBar.rect.width;
        UpdateHealth(_startingHealth);
    }

    public void UpdateHealth(float healthPoints)
    {
        _currentHealth = healthPoints;
        float newWidth = _currentHealth / _startingHealth * _healthBarStartingWidth;
        _playerHealthBar.sizeDelta = new Vector2(newWidth, _playerHealthBar.rect.height);
    }

    public void UpdateScore(int score)
    {
        _gameScore += score;
    }
}
