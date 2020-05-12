using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthWallTriggerDetect : MonoBehaviour
{
    public GameObject collisionParticle;
    bool collidedWithObject = true;

    private void OnTriggerEnter(Collider other)
    {
        if (collidedWithObject == true)
        {
            GameObject collisionSpawned = Instantiate(collisionParticle, transform.position, Quaternion.Euler(new Vector3(0, transform.rotation.y, 0)));
            GetComponentInParent<EarthWall>().EarthSpell(transform.position);
            collidedWithObject = false;
        }
    }
}
