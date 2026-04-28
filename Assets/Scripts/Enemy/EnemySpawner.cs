using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private bool _useLimits;
    [SerializeField] private Transform _upLimit;
    [SerializeField] private Transform _downLimit;
    [SerializeField] private float _duration;
    [SerializeField] private float _startAfter;
    [SerializeField] private float _spawnRate;

    private float _elapsedSeconds;

    private void Start()
    {
        if (_useLimits) StartCoroutine(SpawnEnemyWithLimits());
        else StartCoroutine(SpawnEnemiesWithoutLimits());

    }

    private IEnumerator SpawnEnemyWithLimits()
    {
        yield return new WaitForSeconds(_startAfter);
        StartCoroutine(CountDown());

        if (_duration == 0)
        {
            while (true)
            {
                Vector3 spawnPosition = Vector3.Lerp(_upLimit.position, _downLimit.position, Random.Range(0f, 1f));
                Instantiate(_enemyPrefab, spawnPosition, _enemyPrefab.transform.rotation);

                yield return new WaitForSeconds(_spawnRate);
            }
        }
        else
        {
            while (_elapsedSeconds < _duration)
            {
                Vector3 spawnPosition = Vector3.Lerp(_upLimit.position, _downLimit.position, Random.Range(0f, 1f));
                Instantiate(_enemyPrefab, spawnPosition, _enemyPrefab.transform.rotation);

                yield return new WaitForSeconds(_spawnRate);
            }
        }


        Destroy(gameObject);
    }

    private IEnumerator SpawnEnemiesWithoutLimits()
    {
        yield return new WaitForSeconds(_startAfter);
        StartCoroutine(CountDown());

        if (_duration == 0)
        {
            while (true)
            {
                Instantiate(_enemyPrefab, transform.position, _enemyPrefab.transform.rotation);

                yield return new WaitForSeconds(_spawnRate);
            }
        }
        else
        {
            while (_elapsedSeconds < _duration)
            {
                Instantiate(_enemyPrefab, transform.position, _enemyPrefab.transform.rotation);

                yield return new WaitForSeconds(_spawnRate);
            }
        }

        Destroy(gameObject);
    }

    private IEnumerator CountDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            _elapsedSeconds += 1f;
        }
    }
}