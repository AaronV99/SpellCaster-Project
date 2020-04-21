using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumbers : MonoBehaviour
{

    public float destroyText = 0.5f;

    //moving the instantiate object in the y position
    public Vector3 Offset = new Vector3(0, 3.6f, 0);
    //moving the instantiate object in the x position
    public Vector3 randomOffSet = new Vector3(0.5f, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyText);

        transform.localPosition += Offset;

        //moving text in its local position with a random range 
        transform.localPosition += new Vector3(Random.Range(-randomOffSet.x, randomOffSet.x), Random.Range(-randomOffSet.y, randomOffSet.y), Random.Range(-randomOffSet.z, randomOffSet.z));
    }

}
