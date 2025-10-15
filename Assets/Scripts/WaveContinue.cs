using Unity.Mathematics;
using UnityEngine;
using System.Collections.Generic;

public class WaveContinue : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs; // welke Arda's er zijn
    [SerializeField] private List<int> enemyCounts; // hoeveel Arder's per prefab

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float _spawnInterrval = 6f;

    private float timer = 0f;
    private Queue<GameObject> spawnQueue = new Queue<GameObject>();

    void Start()
    {
        // Build the spawn queue based on enemyCounts
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
