using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject particleSpawned;
    int damage = 50;
    [SerializeField] DamageType damageType;

    void Start()
    {

    }

    public void ExplodeFireBall(Vector3 center)
    {
        Collider[] hitEnemies = Physics.OverlapSphere(center, 25);
        int i = 0;

        while (i < hitEnemies.Length)
        {
            if(hitEnemies[i].gameObject.tag == "Enemy")
            {
                hitEnemies[i].gameObject.GetComponent<AI_Health>().DealDamage(damage, damageType);
            }
            i++;
        }
        Destroy(gameObject);
    }

}
