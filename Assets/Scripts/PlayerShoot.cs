using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private InputActionReference _shootInput;
    [SerializeField] private Transform _spawnPoint;

    public GameObject ProjectilePrefab;
    public float RateOfFire = 0.5f;
    public bool CanShoot = true;

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
        StartCoroutine(ShootDelay());
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(RateOfFire);
        CanShoot = true;
    }
}
