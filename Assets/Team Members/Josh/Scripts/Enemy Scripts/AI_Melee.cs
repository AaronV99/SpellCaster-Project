using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Agent))]

public class AI_Melee : MonoBehaviour
{
    //public GameObject axe;

    public Transform attackPoint;
    public float attackRange; //this value is for the overlapshere size
    public LayerMask targetLayer;
    [SerializeField] private float axeDamage = 50f;

    //Declare an array of colliders that will contain all of the possible collisions we can handle in 1 frame (change value of 10 if there are more or less)
    private Collider[] Collider = new Collider[1];

    //function to call on animation event
    void AxeAttack()
    {
        //draw sphere at attackpoint with the size of attack range
        //We use the OverlapSphereNonAlloc because it uses our already declared collider array, so we are not adding memory every time, like the OverlapSphere function
        int num = Physics.OverlapSphereNonAlloc(attackPoint.position, attackRange, Collider, targetLayer);

        //If we hit something
        if (num > 0)
        {
            //We loop through them
            for (int i = 0; i < num; i++)
            {
                Debug.Log("Hit" + Collider[i].name);
                //deal damage
                Collider[i].GetComponentInParent<healthScript>().damage(axeDamage);
            }
        }
    }


    //visualise the overlapsphere
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


    ////function to be called by animation event to turn on axe collider which will run the ai_axe script
    //void AttackStart()
    //{
    //    axe.GetComponent<Collider>().enabled = true;
    //}

    //void AttackEnd()
    //{
    //    axe.GetComponent<Collider>().enabled = false;
    //}

}
