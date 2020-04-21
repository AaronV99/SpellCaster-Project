using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AI_Projectile : MonoBehaviour
{

    public float damage = 10;

    private healthScript healthScript;

    private void OnTriggerEnter(Collider other)
    {
        //assign the component of the target to the healthScript. If it is null, the GameObject doesn't have that component, so we can't do the damage
        if ((healthScript = other.GetComponentInParent<healthScript>()) != null)
        {
            healthScript.damage(damage);
        }
        if (other.CompareTag("Target"))
        {
            Destroy(gameObject);
        }
    }
}
