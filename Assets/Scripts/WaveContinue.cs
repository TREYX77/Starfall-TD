using Unity.Mathematics;
using UnityEngine;
using System.Collections.Generic;

public class WaveContinue : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs; // Assign multiple prefabs in Inspector
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float _spawnInterrval = 6f;
    [SerializeField] private int _totalEnemies = 10;

    private int enemiesSpawned = 0;
    private float timer = 0f;

    void Update()
    {
        if (enemiesSpawned < _totalEnemies)
        {
            timer += Time.deltaTime;
            if (timer >= _spawnInterrval)
            {
                SpawnEnemy();
                timer = 0f;
            }
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs == null || enemyPrefabs.Count == 0)
            return;

        // Randomly select an enemy prefab to spawn
        int index = UnityEngine.Random.Range(0, enemyPrefabs.Count);
        Instantiate(enemyPrefabs[index], spawnPoint.position, quaternion.identity);
        enemiesSpawned++;
    }
}
