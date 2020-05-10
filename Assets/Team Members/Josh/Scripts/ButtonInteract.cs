using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonInteract : MonoBehaviour {

    public Button aButton;
    public float inputDelay = 0.3f;
    public bool inputDelayed = false;
    float rightGripPressed;
    float leftGripPressed;


    // Use this for initialization
    void Start ()
    {
        aButton = GetComponent<Button>();
   
    }

    void Update()
    {
        rightGripPressed = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch);
        leftGripPressed = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);
    }


    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Finger")
        {
            
            Interact();
        }
        
    }

    void Interact()
    {
        if (rightGripPressed >= 0.7f || leftGripPressed >= 0.7f)
        {
            if (!inputDelayed)
            {
                GetComponent<Image>().color = Color.green;
                aButton.onClick.Invoke();
                SetInputDelay();
            }
        }
    }

    void SetInputDelay()
    {
        inputDelayed = true;
        Invoke("ClearInputDelay", inputDelay);
    }

    void ClearInputDelay()
    {
        GetComponent<Image>().color = Color.white;
        inputDelayed = false;
    }

}
