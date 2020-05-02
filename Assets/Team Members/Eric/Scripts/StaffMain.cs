using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffMain : MonoBehaviour
{
    public GameObject self, staff, dummy, projectile, spawnDummy;
    Vector3 scaleStart = new Vector3(0.5f,0.5f,0.5f), scaleTarget = new Vector3(1,1,1);

    bool isGrabbingStaff, touchingStaff, scaleGateUp;
    float gripPress, triggerPress, staffChargeRate, staffChargeRateMax, growthSpeed = 1;
    public float staffCharge;
    int HandID;

    // Start is called before the first frame update
    void Start()
    {
        staffChargeRate = 2;
        staffChargeRateMax = 6;

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
        switch (HandID)
        {
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

        if (!isGrabbingStaff && touchingStaff && gripPress >= 0.5)
        {
            GrabStaff();
        }
        else if (isGrabbingStaff && gripPress <= 0.5)
        {
            ReleaseStaff(staff);
        }

        if(isGrabbingStaff && triggerPress >= 0.5)
        {
            ChargeStaff();
        }else if(isGrabbingStaff && triggerPress <= 0.5 && staffCharge >= staffChargeRateMax)
        {
            StaffShoot();
        }

        //Error checking for staff grab
        if (gripPress <= 0.5 && staff != null)
        {
            foreach (Transform child in self.transform)
            {
                if (child.tag == "Staff")
                {
                    ReleaseStaff(child.gameObject);
                }
            }
        }

        //Scaling of staff
        if (scaleGateUp)
        {
            if(staff.transform.localScale.x < scaleTarget.x && staff.transform.localScale.z < scaleTarget.z && staff.transform.localScale.y < scaleTarget.y)
            {
                staff.transform.localScale += new Vector3(growthSpeed, growthSpeed, growthSpeed) * Time.deltaTime;

            }

        }
        
    }

    //TO DO NEXT: MAKE STAFF SHOOT THINGS

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Staff")
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
        }
    }

    void GrabStaff()
    {        
        isGrabbingStaff = true;
        scaleGateUp = true;
        staff.transform.SetParent(self.transform);
        staff.transform.position = self.transform.position;
        if (self.tag == "HandR")
        {
            staff.transform.localEulerAngles = new Vector3(self.transform.rotation.x, self.transform.rotation.y, 90);

        }
        else if (self.tag == "HandL")
        {
            staff.transform.localEulerAngles = new Vector3(self.transform.rotation.x, self.transform.rotation.y, -90);

        }
    }

    void ReleaseStaff(GameObject staffObj)
    {                
        staffObj.transform.localScale = scaleStart;
        scaleGateUp = false;
        staff = null;
        staffObj.transform.SetParent(dummy.transform);
        staffObj.transform.position = dummy.transform.position;        
        staffObj.transform.localEulerAngles = new Vector3(23, 0, 11);
        isGrabbingStaff = false;

    }

    void ChargeStaff()
    {
        if (staffCharge < staffChargeRateMax)
        {
            staffCharge += staffChargeRate * Time.deltaTime;
        }
    }

    void StaffShoot()
    {
        if(staffCharge >= staffChargeRateMax)
        {
            //Instantiate(projectile, );
            staffCharge = 0;
        }
    }
}
