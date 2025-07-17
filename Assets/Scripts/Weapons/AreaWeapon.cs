using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaWeapon : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private float _spawnCounter;

    public float cooldown = 5f;
    public float duration = 3f;
    public float damage = 1f;
    public float range = 1f;

    void Update()
    {
        _spawnCounter -= Time.deltaTime;

        if (_spawnCounter > 0) return;

        _spawnCounter = cooldown;
        Instantiate(
            prefab,
            transform.position,
            transform.rotation,
            transform
        );
    }
}