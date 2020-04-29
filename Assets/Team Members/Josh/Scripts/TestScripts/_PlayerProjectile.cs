using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _PlayerProjectile : MonoBehaviour
{

    //Only for testing (MouseAndKeyboard Variant)
    //^
  


    [SerializeField] private float force;
    [SerializeField] private int damage;
    [SerializeField] private DamageType damageType;


    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.GetComponent<AI_Health>() != null)
        {
          col.gameObject.GetComponent<AI_Health>().DealDamage(damage, damageType);
        }
        Destroy(gameObject);
    }
}
