using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellbookArrows : MonoBehaviour
{
    public GameObject bookMain;
    public int orderNumber;
    float pageTurnCooldown, pageTurnCooldownMax = 1;
    bool canTurnPage = true, cooldownStart;

    private void Update()
    {
        if(cooldownStart == true)
        {
            pageTurnCooldown += Time.deltaTime;
            canTurnPage = false;
        }

        if(pageTurnCooldown >= pageTurnCooldownMax)
        {
            pageTurnCooldown = 0;
            canTurnPage = true;
            cooldownStart = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finger" && canTurnPage == true)
        {
            bookMain.GetComponent<Spellbook>().UpdatePages(orderNumber);
            cooldownStart = true;
        }
    }

}
