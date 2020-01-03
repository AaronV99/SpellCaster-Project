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



    // Use this for initialization
    void Start ()
    {
        aButton = GetComponent<Button>();
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
        if (!inputDelayed)
        {
            GetComponent<Image>().color = Color.green;
            aButton.onClick.Invoke();
            SetInputDelay();
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
