using UnityEngine;
using System.Collections.Generic;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private LayerMask _rayLayer;
    [SerializeField] private Material _selectedMaterial;
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private float _spawnYOffset = 0.5f; // Inspector value

    private GameObject _selectedObject;
    private Material _originalMaterial;
    private Dictionary<GameObject, bool> _towerSpots = new Dictionary<GameObject, bool>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _rayLayer))
            {
                GameObject clickedObject = hit.transform.gameObject;

                // Deselect if same object clicked
                if (_selectedObject == clickedObject)
                {
                    _selectedObject.GetComponent<Renderer>().material = _originalMaterial;
                    _selectedObject = null;
                    return;
                }

                if (_selectedObject != null)
                {
                    _selectedObject.GetComponent<Renderer>().material = _originalMaterial;
                }

                _selectedObject = clickedObject;
                _originalMaterial = _selectedObject.GetComponent<Renderer>().material;
                _selectedObject.GetComponent<Renderer>().material = _selectedMaterial;

                // Register spot if not already tracked
                if (!_towerSpots.ContainsKey(_selectedObject))
                {
                    _towerSpots[_selectedObject] = false;
                }
            }
        }
    }

    public void SpawnTower(GameObject towerPrefab)
    {
        if (_selectedObject == null) return;

        // Check if tower already exists on this spot
        if (_towerSpots.ContainsKey(_selectedObject) && _towerSpots[_selectedObject])
        {
            // Already has a tower, do not spawn
            return;
        }

        Instantiate(
            towerPrefab,
            _selectedObject.transform.position + new Vector3(0, _spawnYOffset, 0),
            Quaternion.identity
        );

        _towerSpots[_selectedObject] = true;

        _selectedObject.GetComponent<Renderer>().material = _originalMaterial;
        _selectedObject = null;
    }
}