using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _rateOfFire;
    [SerializeField] private Transform _spawnPoint;

    private bool _canShoot = true;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!_canShoot) return;

        Instantiate(_projectilePrefab, _spawnPoint.position, _projectilePrefab.transform.rotation);

        _canShoot = false;
        _audioSource.Play();

        StartCoroutine(ShootDelay());
    }


    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(_rateOfFire);
        _canShoot = true;
    }
}
