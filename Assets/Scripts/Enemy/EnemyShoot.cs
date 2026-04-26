using System.Collections;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _rateOfFire;
    [SerializeField] private Transform _spawnPoint;

    private bool _canShoot = true;

    private void Update()
    {
        if (!_canShoot) return;

        Instantiate(_projectilePrefab, _spawnPoint.position, _projectilePrefab.transform.rotation);

        _canShoot = false;
        StartCoroutine(ShootDelay());
    }


    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(_rateOfFire);
        _canShoot = true;
    }
}
