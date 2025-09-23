using UnityEngine;

public class Mousecheck : MonoBehaviour
{
    [SerializeField] private LayerMask _rayLayer;
    [SerializeField] private Material _selectedMaterial;
    [SerializeField] private bool _allowMultipleSpawns = true;

    private GameObject _selectedObject;
    private Material _originalMaterial;
    private bool _hasSpawned = false; // houdt bij of er al iets gespawned is

    void Update()
    {
        // Als meerdere spawns niet toegestaan zijn en er al gespawned is, mag niet selecteren
        if (!_allowMultipleSpawns && _hasSpawned) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _rayLayer))
            {
                GameObject clickedObject = hit.transform.gameObject;

                // deselect als dezelfde Cube wordt aangeklikt
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
            }
        }
    }

    public void SpawnTower(GameObject towerPrefab)
    {
        if (_selectedObject == null) return;

        Instantiate(towerPrefab, _selectedObject.transform.position, Quaternion.identity);

        _selectedObject.GetComponent<Renderer>().material = _originalMaterial;
        _selectedObject = null;

        _hasSpawned = true; // voorkomt nieuwe spawns als _allowMultipleSpawns = false
    }
}
