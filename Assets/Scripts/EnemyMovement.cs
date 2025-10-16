using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 670000000f;

    private Transform target;
    private int wavepointIndex = 0;
    void Start()
    {
        if (WaypointsHandler.Waypoints.Count > 0)
            target = WaypointsHandler.Waypoints[0];
        else
            Debug.LogError("no waypoints");
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return; // Prevent null reference errors

        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f) GetNextWaypoint();
    }

    public void GetNextWaypoint()
    {
        if (wavepointIndex >= WaypointsHandler.Waypoints.Count - 1)
        {
            Destroy(gameObject);
            return;
        }
        wavepointIndex++;
        target = WaypointsHandler.Waypoints[wavepointIndex];
    }
}
