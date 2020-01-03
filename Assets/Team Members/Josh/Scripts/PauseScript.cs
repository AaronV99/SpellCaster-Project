using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {

    public GameObject aMenu;
    public bool isOpen;
    public float inputDelay = 0.5f;
    public bool inputDelayed = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(isOpen)
        {
            aMenu.SetActive(true);
        }
        else
        {
            aMenu.SetActive(false);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Finger")
        {
            if (!inputDelayed)
            {
                Debug.Log("Button Pressed");
                SetInputDelay();
                isOpen = !isOpen;
            }
            else
            {
                Debug.Log("Button Pressed but blocked by input delay");
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
        inputDelayed = false;
    }
}
