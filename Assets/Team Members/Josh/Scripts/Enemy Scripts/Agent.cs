using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    public Transform goal;

    [HideInInspector]
    public NavMeshAgent agent;

    public List<Transform> gatePoints;

    public float distToTarget;
    public float maxDistToTarget;

    private Animator animator;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

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
        if (goal != null)
        {
            distToTarget = Vector3.Distance(transform.position, goal.position);

            if (maxDistToTarget <= distToTarget)
            {
                agent.destination = goal.position;

                //failsafe incase we ever have a spell that teleports enemies to a random position
                animator.SetBool("Attack", false);
                agent.isStopped = false;
            }
            else if (agent.remainingDistance <= agent.stoppingDistance)
            {
                agent.isStopped = true;
                animator.SetBool("Attack", true);
            }
        }
        else
        {
            animator.SetTrigger("End");
            agent.isStopped = true;
            agent.ResetPath();
        }
    }
}