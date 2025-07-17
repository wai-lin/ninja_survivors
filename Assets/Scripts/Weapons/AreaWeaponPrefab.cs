using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AreaWeaponPrefab : MonoBehaviour
{
    public AreaWeapon weapon;
    private Vector3 _targetSize = Vector3.one * 3;
    private float _timer;
    public List<EnemyController> enemiesInRange;
    private float _damageCounter;

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

        // damage enemies periodically
        _damageCounter -= Time.deltaTime;
        if (_damageCounter <= 0)
        {
            _damageCounter = weapon.damageSpeed;
            foreach (var enemy in enemiesInRange)
            {
                enemy.TakeDamage(weapon.damage);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            enemiesInRange.Add(other.GetComponent<EnemyController>());
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            enemiesInRange.Remove(other.GetComponent<EnemyController>());
    }
}