using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IceSpike : MonoBehaviour
{
    bool cooldownStart;
    float slowCooldown, slowCooldownMax = 5f;
    int damage = 20;
    [SerializeField] DamageType damageType;
    Collider[] hitEnemies;
    float speedStorage;

    private void Update()
    {
        if(cooldownStart == true)
        {
            slowCooldown += Time.deltaTime;

        }

        if(slowCooldown >= slowCooldownMax)
        {

            RemoveSlow();
            cooldownStart = false;            
            slowCooldown = 0;

        }
    }
    public void IceSpikeSpell(Vector3 center)
    {

        hitEnemies = Physics.OverlapSphere(center, 25);
        int i = 0;

        while (i < hitEnemies.Length)
        {
            if (hitEnemies[i].gameObject.tag == "Enemy")
            {
                hitEnemies[i].gameObject.GetComponent<AI_Health>().DealDamage(damage, damageType);
                speedStorage = hitEnemies[i].gameObject.GetComponent<NavMeshAgent>().speed;
                hitEnemies[i].gameObject.GetComponent<NavMeshAgent>().speed = speedStorage / 2;
            }
            i++;
        }
    }

    void RemoveSlow()
    {
        int i = 0;

        while(i < hitEnemies.Length)
        {
            hitEnemies[i].gameObject.GetComponent<NavMeshAgent>().speed = speedStorage;
            i++;
        }
    }
   
}
