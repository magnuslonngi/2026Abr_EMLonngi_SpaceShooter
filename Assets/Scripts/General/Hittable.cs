using System;
using UnityEngine;

public class Hittable : MonoBehaviour
{
    [SerializeField] public int HealthPoints = 100;

    public void Hit(int damage)
    {
        HealthPoints -= damage;

        if (HealthPoints <= 0)
        {
            HealthPoints = 0;
            Destroy(gameObject);
        }
    }
}