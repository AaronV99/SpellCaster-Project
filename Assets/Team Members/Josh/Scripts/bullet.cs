using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class bullet : MonoBehaviour
{

    public float damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<healthScript>() != null)
        {
            other.GetComponent<healthScript>().damage(damage);
        }
        if (other.tag != "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
