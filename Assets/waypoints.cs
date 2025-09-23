using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class WayPoints : MonoBehaviour
{
    public static List<Transform> Waypoints = new List<Transform>();

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Waypoints.Add(transform.GetChild(i));
            print(Waypoints[i].name);
        }
    }
}

