using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffMain : MonoBehaviour
{
    public GameObject self, staff, dummy, projectile, spawnDummy, spellEffect, collisionEffect;
    Vector3 scaleStart = new Vector3(0.5f,0.5f,0.5f), scaleTarget = new Vector3(1,1,1);

    GameObject particleSpawned;
    public bool isGrabbingStaff;
    bool touchingStaff, scaleGateUp, cooldownStart, canAttack = true;
    float gripPress, triggerPress, growthSpeed = 1;
    public float attackCooldown, attackCooldownMax;
    int HandID, damage;
    [SerializeField]DamageType damageType;

    // Spherecast stuff
    public float rayRadius, maxDistance;
    public GameObject hitObject;
    public LayerMask layerMask;

    Vector3 origin, direction;
    float hitDistance;

    // Start is called before the first frame update
    void Start()
    {
        damage = 10;

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

            } else if(staff.transform.localScale.x > scaleTarget.x && staff.transform.localScale.z > scaleTarget.z && staff.transform.localScale.y > scaleTarget.y)
            {
                scaleGateUp = false;
            }

        }

        //Countdown for basic attack
        if(cooldownStart == true)
        {
            attackCooldown += Time.deltaTime;
            canAttack = false;
        }
        
        if(attackCooldown >= attackCooldownMax)
        {
            attackCooldown = 0;
            canAttack = true;
            Destroy(particleSpawned);
            cooldownStart = false;
        }
    }

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
        if (self.gameObject.GetComponent<BookMain>().isGrabbingBook != true)
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
    }

    void ReleaseStaff(GameObject staffObj)
    {
        if (staff != null)
        {
            staffObj.transform.localScale = scaleStart;
            scaleGateUp = false;
            staff = null;
            staffObj.transform.SetParent(dummy.transform);
            staffObj.transform.position = dummy.transform.position;
            staffObj.transform.localEulerAngles = new Vector3(23, 0, 11);
            isGrabbingStaff = false;
        }

    }

   
    void StaffShoot()
    {
        if (canAttack && isGrabbingStaff)
        {
            origin = spawnDummy.transform.position;
            direction = spawnDummy.transform.forward;
            particleSpawned = Instantiate(spellEffect, spawnDummy.transform.position, spawnDummy.transform.rotation);

            RaycastHit hit;
            print("Shooting");
            if (Physics.SphereCast(origin, rayRadius, direction, out hit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal))
            {
                hitObject = hit.transform.gameObject;
                hitDistance = hit.distance;
                if (hit.transform.gameObject.tag == "Enemy")
                {
                    hit.transform.gameObject.GetComponent<AI_Health>().DealDamage(damage, damageType);
                }

            }
            else
            {
                hitDistance = maxDistance;
                hitObject = null;
            }
            cooldownStart = true;
        }
        //Instantiates projectile
        //Instantiate(projectile, spawnDummy.transform.position, Quaternion.identity);        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + direction * hitDistance);
        Gizmos.DrawWireSphere(origin + direction * hitDistance, rayRadius);
    }
}
