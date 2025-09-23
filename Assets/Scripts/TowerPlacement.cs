using UnityEngine;

// Remove selection logic from here if using TowerCheck on prefabs.
// This script should only handle spawning, not selection.

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private GameObject[] _towerPrefabs; // assign in Inspector
    [SerializeField] private Transform _spawnParent; // optional: parent for spawned towers

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SpawnPrefab(0);
            TowerCheck._isSelected = false; // Deselect after spawning
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SpawnPrefab(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SpawnPrefab(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            SpawnPrefab(3);
    }

    private void SpawnPrefab(int index)
    {
        if (_towerPrefabs != null && _towerPrefabs.Length > index && _towerPrefabs[index] != null)
        {
            var obj = Instantiate(_towerPrefabs[index], transform.position, Quaternion.identity, _spawnParent);
            // Get the TowerCheck component from the spawned object
            TowerCheck towerCheck = obj.GetComponent<TowerCheck>();
            if (towerCheck != null)
            {
                towerCheck.Deselect(); // Call Deselect on the new tower
            }
            else
            {
                Debug.LogWarning("TowerCheck component not found on spawned prefab.");
            }
        }
        else
        {
            Debug.LogWarning("Prefab not assigned or index out of range.");
        }
    }
}