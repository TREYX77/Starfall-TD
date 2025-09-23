using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;  

public class WaypointsHandler : MonoBehaviour
{
    public static List<Transform> Waypoints = new List<Transform>();

    [SerializeField] private List<Transform> debugWaypoints = new List<Transform>();

    void Awake()
    {
        Waypoints.Clear();
        debugWaypoints.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            Waypoints.Add(child);
            debugWaypoints.Add(child); // This will show in Inspector
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}