using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> wavePrefabs; // Assign WaveContinue prefabs here
    [SerializeField] private float timeBetweenWaves = 5f;

    private int currentWaveIndex = 0;
    private bool waveActive = false;
    private WaveContinue currentWaveInstance;

    void Start()
    {
        if (wavePrefabs.Count == 0)
        {
            Debug.LogWarning("No wave prefabs assigned to WaveManager!");
            return;
        }

        StartCoroutine(RunWaves());
    }

    private IEnumerator RunWaves()
    {
        while (currentWaveIndex < wavePrefabs.Count)
        {
            GameObject wavePrefab = wavePrefabs[currentWaveIndex];
            if (wavePrefab == null)
            {
                Debug.LogWarning($"Wave prefab {currentWaveIndex} is null. Skipping.");
                currentWaveIndex++;
                continue;
            }

            // Instantiate the wave prefab
            GameObject waveObj = Instantiate(wavePrefab, transform.position, Quaternion.identity);
            currentWaveInstance = waveObj.GetComponent<WaveContinue>();
            if (currentWaveInstance == null)
            {
                Debug.LogWarning($"Wave prefab {currentWaveIndex} does not have a WaveContinue component. Skipping.");
                Destroy(waveObj);
                currentWaveIndex++;
                continue;
            }

            waveActive = true;
            waveObj.SetActive(true);

            // Wait for wave to finish
            yield return StartCoroutine(WaitForWaveToFinish(currentWaveInstance));

            waveActive = false;
            Destroy(waveObj); // Clean up after wave is done
            yield return new WaitForSeconds(timeBetweenWaves);

            currentWaveIndex++;
        }

        Debug.Log("All waves completed!");
    }

    private IEnumerator WaitForWaveToFinish(WaveContinue wave)
    {
        while (wave.HasEnemiesRemaining())
            yield return null;
    }
}
