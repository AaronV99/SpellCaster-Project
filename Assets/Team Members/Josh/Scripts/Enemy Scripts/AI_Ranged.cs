using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Agent))]

public class AI_Ranged : MonoBehaviour
{
    NavMeshAgent randomDistance;

    public Transform shootingPoint;

    public GameObject spearPrefab;

    public float projectileForce = 1500f;

    Agent enemy;

    void Start()
    {
        enemy = GetComponent<Agent>();
        randomDistance = GetComponent<NavMeshAgent>();
        randomDistance.stoppingDistance = Random.Range(15, 25);
    }

    void shoot()
    {
        GameObject bullet = Instantiate(spearPrefab, shootingPoint.position, Quaternion.LookRotation(transform.forward, transform.up));
        bullet.transform.LookAt(enemy.goal.position);
        bullet.AddComponent<Rigidbody>();
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * projectileForce);
    }

}
