using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject self;
    float launchSpeed = 100f;
    void Start()
    {
        //Starts main launching function
        //LaunchProjectile();
    }

    void Update()
    {
        LaunchProjectile();
    }

    void LaunchProjectile()
    {
        Rigidbody projectileRigidbody = self.GetComponent<Rigidbody>();

        projectileRigidbody.AddForce(Vector3.forward * launchSpeed, ForceMode.Impulse);
    }
}
