using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class healthScript : MonoBehaviour
{
    [SerializeField] float health;
    public float maxHealth = 100f;

    public Slider slider;

    //public bool DestroyAtZero = false;

    //public GameObject gameOverUI;

    void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if(health <= 0 )
        {
            Destroy(gameObject);
            //GameOver();
        }
    }


    public void damage(float damagePoints)
    {
        if (health >= 0)
        {
            health -= damagePoints;

            if (slider != null)
            {
                slider.value = health;
            }
        }
    }

    //void GameOver()
    //{


    //}
}
