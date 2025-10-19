using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _cubeterrain;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _minDistance = 10f;
    [SerializeField] private float _maxDistance = 40f;
    [SerializeField] private float _zoomSensitivity = 10f;

    private float yaw = 0f;
    private float pitch = 0f;
    private float distance = 20f; // Start at midpoint

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
                Debug.Log("Hit object: " + hit.collider.name);
                // Add your selection logic here
            }
        }
    }
}