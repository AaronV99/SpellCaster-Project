using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffMain : MonoBehaviour
{
    public GameObject self, staff;

    bool isGrabbingStaff, touchingStaff;
    float gripPress, triggerPress;
    int HandID;

    // Start is called before the first frame update
    void Start()
    {
        //Sets an ID for each hand to distinguish easily in code.
        if (self.tag == "HandR")
        {
            HandID = 0;
        }
        else if (self.tag == "HandL")
        {
            HandID = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Setting button presses dependand on the hand (left or right)
        switch (HandID) {
            case 0:
                gripPress = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch);
                triggerPress = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch);


                break;

            case 1:
                gripPress = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);
                triggerPress = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch);


                break;

            default:
                print("No hand available");

                break;
        }

        if(!isGrabbingStaff && touchingStaff && gripPress >= 0.5)
        {
            GrabStaff();
        } else if( isGrabbingStaff && gripPress <= 0.5)
        {
            ReleaseStaff();
        }
    }

    //TO DO NEXT: MAKE STAFF SHOOT THINGS, MAKE STAFF ATTACH TO BELT OR BACK

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Staff")
        {
            touchingStaff = true;
            staff = GameObject.FindGameObjectWithTag("Staff");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Staff")
        {
            touchingStaff = false;
            staff = null;
        }
    }

    void GrabStaff()
    {
        staff.transform.SetParent(self.transform);
        staff.transform.position = self.transform.position;
        isGrabbingStaff = true;
    }

    void ReleaseStaff()
    {
        staff.transform.SetParent(null);
        isGrabbingStaff = false;
    }
}
