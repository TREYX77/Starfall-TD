using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _cubeterrain;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _minDistance = 15f;
    [SerializeField] private float _maxDistance = 30f;
    [SerializeField] private float _zoomSensitivity = 2f;

    [Header("Grid Placement")]
    public GridManager gridManager; // Assign in inspector

    private float yaw = 0f;
    private float pitch = 0f;
    private float distance = 20f;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        HandleCameraMovement();
        HandleZoom();
        HandleSelectionAndPlacement();
    }

    private void HandleCameraMovement()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            yaw += mouseX * _rotationSpeed;
            pitch -= mouseY * _rotationSpeed;
            pitch = Mathf.Clamp(pitch, -80f, 80f);
        }

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 offset = rotation * new Vector3(0, 0, -distance);
        transform.position = _cubeterrain.position + offset;
        transform.LookAt(_cubeterrain.position);
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * _zoomSensitivity;
        distance = Mathf.Clamp(distance, _minDistance, _maxDistance);
    }

    private void HandleSelectionAndPlacement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            // Raycast against any collider in the scene
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("Hit object: " + hit.collider.name);

                if (gridManager != null)
                {
                    // Convert hit point to grid coordinates
                    Vector2Int gridPos = gridManager.WorldToGrid(hit.point);

                    // Clamp within grid bounds
                    if (gridPos.x >= 0 && gridPos.x < gridManager.gridWidth &&
                        gridPos.y >= 0 && gridPos.y < gridManager.gridHeight)
                    {
                        // Place road prefab
                        if (gridManager.roadPrefab != null)
                        {
                            gridManager.PlaceObject(gridPos, gridManager.roadPrefab);
                            Debug.Log($"Placed road at {gridPos}");
                        }
                    }
                }
            }
        }
    }
}
