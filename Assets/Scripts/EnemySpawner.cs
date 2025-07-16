using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float _spawnTimer;
    public float spawnInterval;

    void Update()
    {
        _spawnTimer += Time.deltaTime;
        if (_spawnTimer >= spawnInterval)
        {
            _spawnTimer = 0;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, transform.rotation);
    }
}