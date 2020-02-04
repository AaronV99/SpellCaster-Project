using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow : MonoBehaviour
{
    public Vector3 targetSize;
    public float growthSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x < targetSize.x && transform.localScale.y < targetSize.y && transform.localScale.z < targetSize.z)
        {
            
            transform.localScale += new Vector3(growthSpeed,growthSpeed,growthSpeed) * Time.deltaTime;
        }
    }
}
