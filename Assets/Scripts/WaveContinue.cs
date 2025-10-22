using Unity.Mathematics;
using UnityEngine;
using System.Collections.Generic;

public class WaveContinue : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private List<int> enemyCounts;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float _spawnInterrval = 6f;

    private float timer = 0f;
    private Queue<GameObject> spawnQueue = new Queue<GameObject>();

    void Start()
    {
        if (enemyPrefabs != null && enemyCounts != null)
        {
            for (int i = 0; i < enemyPrefabs.Count && i < enemyCounts.Count; i++)
            {
                for (int j = 0; j < enemyCounts[i]; j++)
                {
                    spawnQueue.Enqueue(enemyPrefabs[i]);
                }
            }
        }
    }

    void Update()
    {
        // Do nothing until the game is started
        if (GameManager.Instance == null || !GameManager.Instance.IsGameStarted)
            return;

        if (spawnQueue.Count > 0)
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
        if (spawnQueue.Count == 0)
            return;

        GameObject prefabToSpawn = spawnQueue.Dequeue();
        Instantiate(prefabToSpawn, spawnPoint.position, quaternion.identity);
    }
}
