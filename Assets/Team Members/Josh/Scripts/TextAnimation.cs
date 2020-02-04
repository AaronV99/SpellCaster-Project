using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextAnimation : MonoBehaviour
{

    private OVRGrabbable objGrabbed;
    public  GameObject castleText;
    public GameObject statueText;
    //bool alreadyGrabbed;
    public bool isTouched = false;

    // Start is called before the first frame update
    void Start()
    {
        objGrabbed = GetComponent<OVRGrabbable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (objGrabbed.isGrabbed == true)
        {
            castleText.SetActive(true);
            statueText.SetActive(false);
            if(isTouched == true)
            {
                castleText.SetActive(false);
            }
        }
        else
        {
            castleText.SetActive(false);
        }
    }

    void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.tag == "Target")
        {
            isTouched = true;
            castleText.SetActive(false);
        }
    }
}
