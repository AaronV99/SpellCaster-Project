using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _PlayerShoot : MonoBehaviour
{
   
    //Only for testing (MouseAndKeyboard Variant)


    [SerializeField] private GameObject[] projectiles;
    [SerializeField] private int arrayCount;
    public Transform projectileLoc;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            
            if (arrayCount >= projectiles.Length -1)
            {
                arrayCount = 0;
            }
            else
            {
                arrayCount += 1;
            }
            Debug.Log(projectiles[arrayCount]);
        }


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Player fired");
            GameObject obj = Instantiate(projectiles[arrayCount], projectileLoc.transform.position, projectileLoc.transform.rotation);

        }

    }
}
