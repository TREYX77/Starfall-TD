using Unity.Mathematics;
using UnityEngine;
using System.Collections.Generic;

public class WaveContinue : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs; // welke Arda's er zijn
    [SerializeField] private List<int> enemyCounts; // hoeveel Arder's per prefab

    [SerializeField] private float _spawnInterrval = 6f;

    private float timer = 0f;
    private bool waveFinished = false;
    private Queue<GameObject> spawnQueue = new Queue<GameObject>();

    private bool isActive = false; // Track if the wave is active

    private int aliveEnemies = 0; // Track alive enemies

    private Transform[ ]spawnerTransforms;

    void Awake()
    {
        // Find all GameObjects with the "Spawner" tag and store their transforms
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
        spawnerTransforms = new Transform[spawners.Length];
        for (int i = 0; i < spawners.Length; i++)
        {
            spawnerTransforms[i] = spawners[i].transform;
        }
    }

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

        // Check if wave is finished
        if (spawnQueue.Count == 0 && aliveEnemies == 0)
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
        aliveEnemies = 0;
    }

    private void SpawnEnemy()
    {
        if (spawnQueue.Count > 0 && spawnerTransforms.Length > 0)
        {
            // Pick a random spawner
            int index = UnityEngine.Random.Range(0, spawnerTransforms.Length);
            Transform spawnTransform = spawnerTransforms[index];

            GameObject enemy = Instantiate(spawnQueue.Dequeue(), spawnTransform.position, spawnTransform.rotation);
            aliveEnemies++;
            // Listen for enemy death
            EnemyDeathHandler deathHandler = enemy.AddComponent<EnemyDeathHandler>();
            deathHandler.OnDeath += OnEnemyDeath;
        }
    }

    private void OnEnemyDeath()
    {
        aliveEnemies--;
    }

    public bool HasEnemiesRemaining()
    {
        return spawnQueue.Count > 0 || aliveEnemies > 0;
    }
}

// Helper component to notify when an enemy is destroyed
public class EnemyDeathHandler : MonoBehaviour
{
    public System.Action OnDeath;
    void OnDestroy()
    {
        OnDeath?.Invoke();
    }
}
