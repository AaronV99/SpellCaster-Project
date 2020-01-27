using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Agent))]

public class AI_Ranged : MonoBehaviour
{
    Agent agent;
    Transform player;
    Transform target;

    public float distToPlayer;
    public float maxDistToPlayer;
    public float distToTarget;
    public float maxDistToTarget;
    public Transform shootingPoint;
    public GameObject spearPrefab;
    public float projectileForce = 1500f;

    public string hitCollider;

    private Animator animator;


    void Start()
    {
        agent = GetComponent<Agent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = GameObject.FindGameObjectWithTag("Target").transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        getDistToPlayer();
        getDistToTarget();

        if (distToTarget < maxDistToTarget)
        {

            if (agent.agent.remainingDistance <= 1 + agent.agent.stoppingDistance)
            {
                agent.agent.isStopped = true;
                animator.SetTrigger("Attack");
            }
        }

        if (distToPlayer < maxDistToPlayer)
        {
            agent.run = false;
            agent.agent.destination = player.position;

        }
        else
        {
            agent.run = true;
        }
    }

    void shoot()
    {
            GameObject bullet = Instantiate(spearPrefab, shootingPoint.position, Quaternion.LookRotation(transform.forward, transform.up));
            bullet.AddComponent<Rigidbody>();
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * projectileForce);
    }

    void getDistToPlayer()
    {
        distToPlayer = Vector3.Distance(transform.position, player.position);
    }

    void getDistToTarget()
    {
        distToTarget = Vector3.Distance(transform.position, target.position);
    }

    public string raycastTag()
    {
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(new Ray(shootingPoint.position, transform.forward), out hit, maxDistToPlayer))
        {
            hitCollider = hit.transform.tag;
            Debug.DrawLine(transform.position, hit.point, Color.white);
            return hit.transform.tag;

        }
        else
        {
            return null;
        }
    }

}
