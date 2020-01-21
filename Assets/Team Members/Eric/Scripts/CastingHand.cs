using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CastingHand : MonoBehaviour
{
    public GameObject self, castingPlane;
    int HandID;
    float gripPress, resetTImer = 0.5f;
    bool castButtonPress, triggerTouch;
    static bool castButtonPress_all;
    public bool isCasting;
    List<GameObject> nodes = new List<GameObject>();
    void Start()
    {
        //Sets an ID for each hand to distinguish easily in code.
        if(self.tag == "HandR"){
            HandID = 0;
        } else if(self.tag == "HandL")
        {
            HandID = 1;
        }

        foreach(GameObject node in GameObject.FindGameObjectsWithTag("CastingNode"))
        {
            nodes.Add(node);
        }

    }

    void Update()
    {
        //Uses switch case from Hand ID (Left or Right)
        switch (HandID)
        {
            case 0:
                //Checks if: Grip is pressed, A or X is pressed (depends on hand) and if trigger isn't touched (pointing)
                gripPress = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch);
                castButtonPress = OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTouch);
                triggerTouch = (OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger, OVRInput.Controller.RTouch));

                if (gripPress >= 0.7f && castButtonPress && triggerTouch != true)
                {
                    isCasting = true;
                    //print("Casting Right=" + isCasting);
                } else { 
                    isCasting = false;
                    
                }
                break;

            case 1:        
                gripPress = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);
                castButtonPress = OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.LTouch);
                triggerTouch = (OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger, OVRInput.Controller.LTouch));
                if (gripPress >= 0.7f && castButtonPress && triggerTouch != true){
                    isCasting = true;
                    //print("Casting Left=" + isCasting);
                } else {
                    isCasting = false;
                    
                }
               break;

            default:
                print("No hand available");
                break;
        }
       
        if (OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.LTouch) || OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            castButtonPress_all = true;
            castingPlane.SetActive(true);
        }else if(!OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.LTouch) || !OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            castButtonPress_all = false;
        }

        resetTImer -= Time.deltaTime;

        if (resetTImer <= 0f)
        {
            if (!castButtonPress_all && castingPlane.activeSelf)
            {
                foreach (GameObject node in GameObject.FindGameObjectsWithTag("CastingNode"))
                {
                    node.GetComponent<NodeTriggerDetect>().NodeReset();
                }
                castingPlane.SetActive(false);

            }
            //print("Timer Reset");
            resetTImer = 0.5f;
        }

        if(gripPress >= 0.5 /*&& gameObject.GetComponentInParent<OVRGrabber>().isGrabbing == false*/)
        {
            gameObject.GetComponentInParent<Telekinesis>().PullObject();
        } else if( gripPress <= 0.5f && gameObject.GetComponentInParent<Telekinesis>().isPulling == true)
        {
            gameObject.GetComponentInParent<Telekinesis>().DropObject();
        }
    }

    
}
