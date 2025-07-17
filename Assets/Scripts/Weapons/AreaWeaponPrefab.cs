using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaWeaponPrefab : MonoBehaviour
{
    public AreaWeapon weapon;
    private Vector3 _targetSize = Vector3.one * 3;
    private float _timer;
    
    void Start()
    {
        weapon = GameObject.Find("AreaWeapon").GetComponent<AreaWeapon>();
        transform.localScale = Vector3.zero;
        _timer = weapon.duration;
        _targetSize *= weapon.range;
    }

    void Update()
    {
        // grow and shrink towards target size
        transform.localScale = Vector3.MoveTowards(
            transform.localScale,
            _targetSize * weapon.range,
            Time.deltaTime * 5
        );
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _targetSize = Vector3.zero;
            if (transform.localScale.x == 0) Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    { 
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            enemy.TakeDamage(weapon.damage);
        }
    }
}