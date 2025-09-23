using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraMovement : MonoBehaviour
{


    [SerializeField] private Transform _cubeterrain;
    [SerializeField] private float _rotationSpeed = 10f;

    [SerializeField] private float _minDistance = 15f;
    [SerializeField] private float _maxDistance = 30f;
    [SerializeField] private float _zoomSensitivity = 2f;

    private float yaw = 0f;
    private float pitch = 0f;
    private float distance = 22.5f;
    void Update()
    {

        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            yaw += mouseX * _rotationSpeed;
            pitch -= mouseY * _rotationSpeed;

            //clamp to prevent camera from flipping
            pitch = Mathf.Clamp(pitch, 5f, 89f);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * _zoomSensitivity;
        distance = Mathf.Clamp(distance, _minDistance, _maxDistance);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 offset = rotation * new Vector3(0, 0, -distance); // zoom camera in and out by using scrollwheel

        transform.position = _cubeterrain.position + offset;
        transform.LookAt(_cubeterrain.position);
    }
}