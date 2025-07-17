using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaWeapon : Weapon
{
    [SerializeField] private GameObject prefab;
    private float _spawnCounter;

    void Update()
    {
        _spawnCounter -= Time.deltaTime;

        if (_spawnCounter > 0) return;

        _spawnCounter = stats[weaponLevel - 1].cooldown;
        Instantiate(
            prefab,
            transform.position,
            transform.rotation,
            transform
        );
    }
}