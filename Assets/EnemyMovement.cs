using UnityEngine;
using System.Collections;


public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f;

    private Transform target;
    private int wavepointIndex = 0;
    void Start()
    {
        target = WayPoints.Waypoints[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWayPoint();
        }
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

