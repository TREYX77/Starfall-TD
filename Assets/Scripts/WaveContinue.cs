using Unity.Mathematics;
using UnityEngine;

public class WaveContinue : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float _spawnInterrval = 6f;
    [SerializeField] private int _totalCubes = 10;

    private int cubesSpawned = 0;
    private float timer = 0f;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (cubesSpawned < _totalCubes)
        {
            timer += Time.deltaTime;
            if (timer >= _spawnInterrval)
            {
                SpawnCube();
                timer = 0f;
            }
        }
    }

    void SpawnCube()
    {
        Instantiate(cubePrefab, spawnPoint.position, quaternion.identity);
        cubesSpawned++;
    }
}
