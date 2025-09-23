using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _cubeterrain;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _minDistance = 15f;
    [SerializeField] private float _maxDistance = 30f;
    [SerializeField] private float _zoomSensitivity = 2f;

    private float yaw = 0f;
    private float pitch = 0f;
    private float distance = 20f; // Start at midpoint

    private TowerCheck _selectedTowerGround;

    void Update()
    {
        // Camera movement
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            yaw += mouseX * _rotationSpeed;
            pitch -= mouseY * _rotationSpeed;
            pitch = Mathf.Clamp(pitch, -80f, 80f);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * _zoomSensitivity;
        distance = Mathf.Clamp(distance, _minDistance, _maxDistance);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 offset = rotation * new Vector3(0, 0, -distance);

        transform.position = _cubeterrain.position + offset;
        transform.LookAt(_cubeterrain.position);

        // Selection logic for tower ground
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                var towerCheck = hit.collider.GetComponent<TowerCheck>();
                if (towerCheck != null)
                {
                    if (_selectedTowerGround != null && _selectedTowerGround != towerCheck)
                        _selectedTowerGround.Deselect();

                    if (towerCheck == _selectedTowerGround)
                    {
                        towerCheck.Deselect();
                        _selectedTowerGround = null;
                        Debug.Log($"Tower ground {towerCheck.gameObject.name} deselected.");
                    }
                    else
                    {
                        towerCheck.Select();
                        _selectedTowerGround = towerCheck;
                        Debug.Log($"Tower ground {towerCheck.gameObject.name} selected.");
                    }
                }
            }
        }

        // Spawning logic
        if (_selectedTowerGround != null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                _selectedTowerGround.SpawnPrefabAtTowerGround(0);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                _selectedTowerGround.SpawnPrefabAtTowerGround(1);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                _selectedTowerGround.SpawnPrefabAtTowerGround(2);
            if (Input.GetKeyDown(KeyCode.Alpha4))
                _selectedTowerGround.SpawnPrefabAtTowerGround(3);
        }
    }
}

