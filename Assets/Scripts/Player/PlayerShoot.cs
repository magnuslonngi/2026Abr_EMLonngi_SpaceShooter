using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private InputActionReference _shootInput;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private int _maxPowerUpsPicks = 2;

    public GameObject ProjectilePrefab;
    public float RateOfFire = 0.5f;
    public bool CanShoot = true;
    public int CurrentPowerUpsApplied = 0;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _shootInput.action.Enable();
    }

    private void Update()
    {
        bool isShooting = _shootInput.action.ReadValue<float>() > 0 ? true : false;
        if (!isShooting) return;

        OnShoot();
    }

    private void OnDisable()
    {
        _shootInput.action.Disable();
    }

    private void OnShoot()
    {
        if (!CanShoot) return;

        Instantiate(ProjectilePrefab, _spawnPoint.position, ProjectilePrefab.transform.rotation);
        
        CanShoot = false;
        _audioSource.Play();

        StartCoroutine(ShootDelay());
    }

    public IEnumerator IncreaseFireRate(float duration, float increasedFireRate)
    {
        PlayerShoot[] playerShoots = GetComponentsInChildren<PlayerShoot>();
        bool isEmpty = playerShoots.Length == 0 ? true : false;
        bool cantPowerUp = playerShoots[0].CurrentPowerUpsApplied >= playerShoots[0]._maxPowerUpsPicks;

        if (!cantPowerUp)
        {
            if (!isEmpty)
            {
                foreach (var playerShoot in playerShoots)
                {
                    playerShoot.RateOfFire -= increasedFireRate;
                    playerShoot.CurrentPowerUpsApplied++;
                }
            }

            yield return new WaitForSeconds(duration);

            if (!isEmpty)
            {
                foreach (var playerShoot in playerShoots)
                {
                    playerShoot.RateOfFire += increasedFireRate;
                    playerShoot.CurrentPowerUpsApplied--;
                }
            }
        }

        yield return null;
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(RateOfFire);
        CanShoot = true;
    }
}
