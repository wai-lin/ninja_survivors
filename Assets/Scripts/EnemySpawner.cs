using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public GameObject enemyPrefab;
        public float spawnTimer;
        public float spawnInterval;
        public int enemiesPerWave;
        public int spawnedEnemiesCount;
    }

    public List<Wave> waves;
    public int waveNumber;
    private Wave _currentWave;
    public Transform minSpawnPos;
    public Transform maxSpawnPos;

    void Update()
    {
        _currentWave = waves[waveNumber];

        _currentWave.spawnTimer += Time.deltaTime;
        if (_currentWave.spawnTimer >= _currentWave.spawnInterval)
        {
            _currentWave.spawnTimer = 0;
            SpawnEnemy();
        }

        if (_currentWave.spawnedEnemiesCount >= _currentWave.enemiesPerWave)
        {
            _currentWave.spawnedEnemiesCount = 0;
            if (_currentWave.spawnInterval > 0.3f)
                _currentWave.spawnInterval *= 0.95f; // spawn rate 5% faster on next wave
            waveNumber++;
        }

        if (waveNumber >= waves.Count)
        {
            waveNumber = 0;
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(
            _currentWave.enemyPrefab,
            RandomSpawnPoint(),
            transform.rotation
        );
        _currentWave.spawnedEnemiesCount++;
    }

    private Vector2 RandomSpawnPoint()
    {
        Vector2 spawnPoint;

        if (Random.Range(0f, 1f) > 0.5f)
        {
            spawnPoint.x = Random.Range(minSpawnPos.position.x, maxSpawnPos.position.x);
            spawnPoint.y = Random.Range(0f, 1f) > 0.5f ? minSpawnPos.position.y : maxSpawnPos.position.y;
        }
        else
        {
            spawnPoint.y = Random.Range(minSpawnPos.position.y, maxSpawnPos.position.y);
            spawnPoint.x = Random.Range(0f, 1f) > 0.5f ? minSpawnPos.position.x : maxSpawnPos.position.x;
        }

        return spawnPoint;
    }
}