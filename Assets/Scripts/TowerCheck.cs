using UnityEngine;

public class TowerCheck : MonoBehaviour
{
    [SerializeField] private GameObject _Tower;
    [SerializeField] private GameObject _towerGround; // ground tower
    [SerializeField] public static bool _isSelected = false;

    [SerializeField] private GameObject[] _towerPrefabs; // assign 4 prefabs in Inspector

    void Update()
    {
        // No input logic here; handled by CameraMovement
    }

    public void Select()
    {
        _isSelected = true;
        var renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.green;  
        }
    }

    public void Deselect()
    {
        _isSelected = false;
        var renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.white;
        }
    }

    public void SpawnPrefabAtTowerGround(int prefabIndex)
    {
        if (_towerPrefabs != null && _towerPrefabs.Length > prefabIndex && _towerPrefabs[prefabIndex] != null && _towerGround != null)
        {
            Instantiate(_towerPrefabs[prefabIndex], _towerGround.transform.position, Quaternion.identity);
            Debug.Log($"Prefab {prefabIndex + 1} spawned at tower ground position.");
        }
        else
        {
            Debug.LogWarning("Prefab or tower ground not assigned, or index out of range.");
        }
    }
}