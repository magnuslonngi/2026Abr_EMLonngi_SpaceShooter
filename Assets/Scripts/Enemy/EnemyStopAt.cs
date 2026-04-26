using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class EnemyStopAt : MonoBehaviour
{
    [SerializeField] private float _stopAtHorizontal;

    private EnemyMovement _enemyMovement;

    private void Awake()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        if (transform.position.x < _stopAtHorizontal) _enemyMovement.StopHorizontalMovement();
    }
}
