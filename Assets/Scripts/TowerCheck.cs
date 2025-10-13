using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private LayerMask _spawnLayer;   // alleen spawnpoints
    [SerializeField] private Material _selectedMaterial;
    [SerializeField] private Vector3 _towerOffset = new Vector3(0, 0.5f, 0);

    private GameObject _selectedSpawnPoint;
    private Material _originalMaterial;

    void Update()
    {
        // Klik op spawnpoint om te selecteren
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _spawnLayer))
            {
                GameObject clickedTile = hit.transform.gameObject;

                // Deselecet als dezelfde tile wordt aangeklikt
                if (_selectedSpawnPoint == clickedTile)
                {
                    RestoreMaterial(_selectedSpawnPoint);
                    _selectedSpawnPoint = null;
                    return;
                }

                // Revert vorige selectie
                if (_selectedSpawnPoint != null)
                    RestoreMaterial(_selectedSpawnPoint);

                // Selecteer nieuwe tile
                _selectedSpawnPoint = clickedTile;
                SetMaterial(_selectedSpawnPoint, _selectedMaterial);
            }
        }
    }

    // Roep dit aan vanuit de UI-knop om een tower te spawnen
    public void SpawnTower(GameObject towerPrefab)
    {
        if (_selectedSpawnPoint == null || towerPrefab == null) return;

        // Voeg SpawnPoint component toe als ontbreekt
        SpawnPoint sp = _selectedSpawnPoint.GetComponent<SpawnPoint>();
        if (sp == null) sp = _selectedSpawnPoint.AddComponent<SpawnPoint>();

        // Check of al bezet
        if (sp.IsOccupied)
        {
            Debug.Log("Spawnpoint already has a tower!");
            return;
        }

        // Spawn tower
        Instantiate(towerPrefab, _selectedSpawnPoint.transform.position + _towerOffset, Quaternion.identity);
        sp.IsOccupied = true;

        // Reset selectie
        RestoreMaterial(_selectedSpawnPoint);
        _selectedSpawnPoint = null;
    }

    private void RestoreMaterial(GameObject obj)
    {
        Renderer rend = obj.GetComponent<Renderer>();
        if (rend != null && _originalMaterial != null)
            rend.material = _originalMaterial;
    }

    private void SetMaterial(GameObject obj, Material mat)
    {
        Renderer rend = obj.GetComponent<Renderer>();
        if (rend != null)
        {
            _originalMaterial = rend.material;
            rend.material = mat;
        }
    }
}

// Simpel SpawnPoint component voor elk tile
public class SpawnPoint : MonoBehaviour
{
    [HideInInspector] public bool IsOccupied = false;
}
