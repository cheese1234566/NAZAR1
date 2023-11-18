using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent nMA;
    public GameObject[] waypoints;

    private int currentWaypointIndex;

    private void Start()
    {
        nMA.SetDestination(waypoints[0].transform.position);
    }

    private void Update()
    {
        if (nMA.remainingDistance < nMA.stoppingDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            nMA.SetDestination(waypoints[currentWaypointIndex].transform.position);
        }
    }
}
