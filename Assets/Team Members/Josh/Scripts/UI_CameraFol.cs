using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CameraFol : MonoBehaviour
{

    // Testing purposes only (For KeyboardAndMouse Variant)

    public Transform cam;


    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }


}
