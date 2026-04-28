using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject[] _hideOnEnd;
    [SerializeField] private RectTransform _playerHealthBar;
    [SerializeField] private Text _playerScoreText;
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private Text _gameOverScore;
    [SerializeField] private int _firstUpgradeScore;
    [SerializeField] private GameObject _firstUpgrade;
    [SerializeField] private int _secondUpgradeScore;
    [SerializeField] private GameObject _secondUpgrade;

    private int _gameScore = 0;

    private float _startingHealth;
    private float _currentHealth;
    private float _healthBarStartingWidth;
    private bool _gameEnded;
    private bool _firstUpgradeUnlocked;
    private bool _secondUpgradeUnlocked;

    private void Awake()
    {
        _currentHealth = _startingHealth = _player.GetComponent<Hittable>().HealthPoints;
        _healthBarStartingWidth = _playerHealthBar.rect.width;

        UpdateHealth(_startingHealth);
        UpdateScore(0);
    }

    private void Update()
    {
        if (_gameEnded) return;

        if (_player == null)
        {
            _gameEnded = true;
            _gameOverScreen.SetActive(true);
            _gameOverScore.text += _gameScore.ToString();

            AudioListener.volume = 0;

            foreach (var gameObject in _hideOnEnd)
            {
                gameObject.SetActive(false);
            }
        }


        if (_gameScore >= _firstUpgradeScore && !_firstUpgradeUnlocked)
        {
            _firstUpgradeUnlocked = true;
            _firstUpgrade.SetActive(true);
        }

        if (_gameScore >= _secondUpgradeScore && !_secondUpgradeUnlocked)
        {
            _secondUpgradeUnlocked = true;
            _secondUpgrade.SetActive(true);
        }
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
        _playerScoreText.text =  _gameScore.ToString();
    }

    public void TryAgain()
    {
        AudioListener.volume = 1;
        SceneManager.LoadScene(1);
    }

    public void GoBackToMenu()
    {
        AudioListener.volume = 1;
        SceneManager.LoadScene(0);
    }
}
