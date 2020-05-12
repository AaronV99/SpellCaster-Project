using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthWall : MonoBehaviour
{
    public GameObject particleSpawned;
    int damage = 20;
    [SerializeField] DamageType damageType;

    private void Start()
    {
        particleSpawned.GetComponent<Rigidbody>().AddForce(transform.forward * 1500);
    }

   
    public void EarthSpell(Vector3 center)
    {
        Collider[] hitEnemies = Physics.OverlapSphere(center, 15);
        int i = 0;

        while (i < hitEnemies.Length)
        {
            if (hitEnemies[i].gameObject.tag == "Enemy")
            {
                hitEnemies[i].gameObject.GetComponent<AI_Health>().DealDamage(damage, damageType);
            }
            i++;
        }
        Destroy(gameObject);
    }
    
}
