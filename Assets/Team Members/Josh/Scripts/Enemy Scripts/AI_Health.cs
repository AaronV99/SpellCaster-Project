using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AI_Health : MonoBehaviour
{

    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth = 100;

    public Resistances damageResist;

    private bool isDead = false;

    public Slider slider;

    public GameObject damageNumbersPrefab;

    private Animator anim;



    void Start()
    {
        anim = GetComponent<Animator>();

        currentHealth = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = currentHealth;
    }


    
    public void DealDamage(int damage, DamageType damageType)
    {

        //take in the damage and damage type from the scriptableObject 
        currentHealth -= damageResist.TotalDamageTaken(damage, damageType);

        slider.value = currentHealth;

        
        if(damageNumbersPrefab && currentHealth > 0)
        {
            FloatingDamageNumbers(damage, damageType);
        }

        if(currentHealth <= 20)
        {
            Enraged();

            if (currentHealth <= 0 && !isDead)
            {
                Die();
            }
        }
        
    }


    void Enraged()
    {
        anim.SetTrigger("Enraged");
    }

    //method called to instantiate damage numbers 
    void FloatingDamageNumbers(int  damage, DamageType damageType)
    {
        GameObject numbers = Instantiate(damageNumbersPrefab, transform.position, Quaternion.identity, transform);
        numbers.GetComponent<TextMesh>().text = damageResist.TotalDamageTaken(damage, damageType).ToString();

    }

    void Die()
    {
        isDead = true;

        spawnManagerV3.EnemiesAlive--;

        Destroy(gameObject);
    }


}
