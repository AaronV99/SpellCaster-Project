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

    private NavMeshObstacle obstacle;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        obstacle = GetComponent<NavMeshObstacle>();

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


        // Obstacle avoidance- turns off navmesh agent component and enable obstacle avoidance so enemies will go round agents
        if (agent.isStopped == true)
        {
            obstacle.carving = true;
            obstacle.carveOnlyStationary = true;
            this.GetComponent<NavMeshAgent>().enabled = false;
            this.GetComponent<NavMeshObstacle>().enabled = true;
        }
    }
}