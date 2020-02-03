using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telekinesis : MonoBehaviour
{
    // Start is called before the first frame update

    float speed = 5;
    public float TargetDistance;
    public GameObject indicatorPrefab, targetObject;
    public Transform dummy;
    bool iconSpawned = false;
    public bool isPulling = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug Raycast
        Vector3 forward = dummy.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(dummy.position, forward, Color.green);
        
        if (Physics.Raycast(dummy.position, dummy.TransformDirection(Vector3.forward),out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Grabbable") && !iconSpawned)
            {
                //Debugging
                Instantiate(indicatorPrefab, hit.transform.position, Quaternion.identity);
                iconSpawned = true;
                targetObject = hit.collider.gameObject;
            } else if (hit.collider.tag != "Grabbable" && iconSpawned)
            {
                targetObject = null;
                DestroyParticles();
            }

            TargetDistance = hit.distance;
            //print(TargetDistance + " " + hit.collider.gameObject.name);
        }
        

    }

    public void PullObject()
    {

        targetObject.GetComponent<Rigidbody>().isKinematic = true;
        targetObject.transform.position = Vector3.MoveTowards(targetObject.transform.position, dummy.transform.position, speed * Time.deltaTime);
        isPulling = true;
    }

    public void DropObject()
    {
        isPulling = false;
        targetObject.GetComponent<Rigidbody>().isKinematic = false;
        targetObject = null;
    }

    void DestroyParticles()
    {
        GameObject[] particles = GameObject.FindGameObjectsWithTag("TelekinesisIcon");

        foreach(GameObject target in particles)
        {
            Destroy(target);
        }                
        iconSpawned = false;

    }
}
