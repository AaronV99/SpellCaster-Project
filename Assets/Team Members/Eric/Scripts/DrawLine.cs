using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    LineRenderer lineRenderer;
    float counter, distance;

    public Transform origin, destination;
    public float drawSpeed = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = .010f;
        lineRenderer.endWidth = .010f;

    }

    // Update is called once per frame
    void Update()
    {
        if (lineRenderer.positionCount >= 0)
        {
            distance = Vector3.Distance(origin.position, destination.position);
            if (counter < distance)
            {
                counter += .1f / drawSpeed;

                float x = Mathf.Lerp(0, distance, counter);

                Vector3 pointA = origin.position;
                Vector3 pointB = destination.position;

                Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;

                lineRenderer.SetPosition(1, pointAlongLine);
                
            }
        }
    }
}
