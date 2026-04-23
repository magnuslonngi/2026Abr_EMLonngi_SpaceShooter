using System;
using UnityEngine;

public class Hittable : MonoBehaviour
{
    [SerializeField] private int _healthPoints = 100;

    private Collider2D _collider2D;

    public void Hit(int damage)
    {
        _healthPoints -= damage;

        if (_healthPoints < 0)
        {
            _healthPoints = 0;
        }
    }
}