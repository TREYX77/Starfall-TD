using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Enemy enemy;
    private Transform target;
    private int waypointIndex = 0;
    public float rotationSpeed = 10f;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        if (WaypointsHandler.Waypoints.Count > 0)
            target = WaypointsHandler.Waypoints[0];
        else
            Debug.LogError("No waypoints found for enemy movement!");
    }

    void Update()
    {
        if (target == null || enemy == null) return;

        Vector3 dir = target.position - transform.position;

        // Smooth rotation toward target
        if (dir != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }

        // Move forward
        transform.Translate(Vector3.forward * enemy.speed * Time.deltaTime, Space.Self);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
            GetNextWaypoint();
    }

    void GetNextWaypoint()
    {
        if (waypointIndex >= WaypointsHandler.Waypoints.Count - 1)
        {
            Destroy(gameObject);
            return;
        }

        waypointIndex++;
        target = WaypointsHandler.Waypoints[waypointIndex];
    }
}
