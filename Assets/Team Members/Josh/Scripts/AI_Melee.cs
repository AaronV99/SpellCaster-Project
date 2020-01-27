using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Agent))]

public class AI_Melee : MonoBehaviour
{
    private Animator animator;
    Agent agent;
    Transform target;
    public float distToTarget;
    public float maxDistToTarget;
    public GameObject enemy_axe;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<Agent>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Target").transform;
    }

    // Update is called once per frame
    void Update()
    {
        getDistToTarget();

        if(distToTarget < maxDistToTarget)
        {

            if (agent.agent.remainingDistance <= 1 + agent.agent.stoppingDistance)
            {
                agent.agent.isStopped = true;
                animator.SetTrigger("Attack");
            }
        }
    }

    void getDistToTarget()
    {
        distToTarget = Vector3.Distance(transform.position, target.position);
    }

    public void AttackStart()
    {
        enemy_axe.GetComponent<Collider>().enabled = true;
        Debug.Log("Hit");
    }

    public void AttackEnd()
    {
        enemy_axe.GetComponent<Collider>().enabled = false;
    }

}
