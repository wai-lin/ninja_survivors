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
        // don't spawn enemies when player is dead
        if (!Player.Instance.gameObject.activeSelf) return;
        
        _currentWave = waves[waveNumber];

        // increment spawnTimer and reset periodically
        _currentWave.spawnTimer += Time.deltaTime;
        if (_currentWave.spawnTimer >= _currentWave.spawnInterval)
        {
            _currentWave.spawnTimer = 0;
            SpawnEnemy();
        }

        // reset spawn count after reaching the wave spawn count,
        // reduce the spawnInterval by 15% for next wave to increase difficulty,
        // and proceed to next wave
        if (_currentWave.spawnedEnemiesCount >= _currentWave.enemiesPerWave)
        {
            _currentWave.spawnedEnemiesCount = 0;
            if (_currentWave.spawnInterval > 1.5f)
                _currentWave.spawnInterval *= 0.85f; // spawn rate 15% faster on next wave
            waveNumber++;
        }

        // reset to wave 1 when all the wave are spawned
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