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
    private bool waveFinished = false;
    private Queue<GameObject> spawnQueue = new Queue<GameObject>();

    private bool isActive = false; // Track if the wave is active

    void OnEnable()
    {
        isActive = true;
        InitializeSpawnQueue();
    }

    void OnDisable()
    {
        isActive = false;
    }

    void Update()
    {
        if (!isActive || waveFinished) return; // Only run if the wave is active

        timer += Time.deltaTime;
        if (timer >= _spawnInterrval && spawnQueue.Count > 0)
        {
            SpawnEnemy();
            timer = 0f;
        }

        if (spawnQueue.Count == 0)
        {
            waveFinished = true;
        }
    }

    private void InitializeSpawnQueue()
    {
        spawnQueue.Clear();
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            for (int j = 0; j < enemyCounts[i]; j++)
            {
                spawnQueue.Enqueue(enemyPrefabs[i]);
            }
        }
        waveFinished = false;
    }

    private void SpawnEnemy()
    {
        if (spawnQueue.Count > 0)
        {
            GameObject enemy = Instantiate(spawnQueue.Dequeue(), spawnPoint.position, spawnPoint.rotation);
            // Additional enemy setup logic here
        }
    }

    public bool HasEnemiesRemaining()
    {
        return spawnQueue.Count > 0;
    }
}
