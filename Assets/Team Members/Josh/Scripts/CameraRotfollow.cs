using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotfollow : MonoBehaviour {


    public Camera my_camera;


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        //transform.LookAt(transform.position + my_camera.transform.rotation * Vector3.forward, my_camera.transform.rotation * Vector3.up);

        Vector3 targetPosition = new Vector3(my_camera.transform.position.x, transform.position.y, my_camera.transform.position.z);
        transform.LookAt(targetPosition);
    }
}

