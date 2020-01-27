using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class healthScript : MonoBehaviour
{
    public float health = 100;
    public float damageMultiplier = 1;
    public Image UIbar;
    public bool destroyAfterDie = false;
    public GameObject gameOverUI;

    public void damage(float damagePoints)
    {
        if (health- damagePoints * damageMultiplier >= 0)
        {
            health -= damagePoints * damageMultiplier;
            if (UIbar != null)
            {
                UIbar.fillAmount = health / 100;
            }
        }
        else 
        {
            if (gameOverUI != null)
            {
                gameOverUI.SetActive(true);
            }

            if (destroyAfterDie)
                Destroy(gameObject);
        }

        
    }
}
