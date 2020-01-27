using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    public Transform goal;

    [HideInInspector]
    public NavMeshAgent agent;

    public List<Transform> gatePoints;

    [HideInInspector]
    public bool run = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        //store every gameobject in scene with tag 'node' into the list
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Waypoint"))
        {
            gatePoints.Add(go.GetComponent<Transform>());
        }
        //choose 1 random gameobject in the list
        goal = gatePoints[Random.Range(0, gatePoints.Count)];

    }

    void Update()
    {
        if (run)
        {
            agent.destination = goal.position;
        }
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            transform.LookAt(agent.destination + new Vector3(0, 3));
        }
    }
}