using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AreaWeaponPrefab : MonoBehaviour
{
    public AreaWeapon weapon;
    private WeaponStats _currentWeaponStats;
    
    private Vector3 _targetSize = Vector3.one * 3;
    private float _timer;
    public List<Enemy> enemiesInRange;
    private float _damageCounter;

    void Start()
    {
        weapon = GameObject.Find("AreaWeapon").GetComponent<AreaWeapon>();
        transform.localScale = Vector3.zero;

        _currentWeaponStats = weapon.stats[weapon.weaponLevel - 1];
        _timer = _currentWeaponStats.duration;
        _targetSize *= _currentWeaponStats.range;
        AudioController.Instance.PlayModifiedSound(AudioController.Instance.weaponSpawn);
    }

    void Update()
    {
        // grow and shrink towards target size
        transform.localScale = Vector3.MoveTowards(
            transform.localScale,
            _targetSize * _currentWeaponStats.range,
            Time.deltaTime * 5
        );
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _targetSize = Vector3.zero;
            if (transform.localScale.x == 0)
            {
                Destroy(gameObject);
                AudioController.Instance.PlayModifiedSound(AudioController.Instance.weaponDespawn);
            }
        }

        // damage enemies periodically
        _damageCounter -= Time.deltaTime;
        if (_damageCounter <= 0)
        {
            _damageCounter = _currentWeaponStats.damageSpeed;
            for (int i = 0; i < enemiesInRange.Count; i++)
            {
                enemiesInRange[i].TakeDamage(_currentWeaponStats.damage);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            enemiesInRange.Add(other.GetComponent<Enemy>());
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            enemiesInRange.Remove(other.GetComponent<Enemy>());
    }
}