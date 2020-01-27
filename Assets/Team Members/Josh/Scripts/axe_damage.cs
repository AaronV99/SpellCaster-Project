using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axe_damage : MonoBehaviour
{

    public float damage = 50;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<healthScript>() != null)
        {
            other.GetComponent<healthScript>().damage(damage);
        }
    }
}
