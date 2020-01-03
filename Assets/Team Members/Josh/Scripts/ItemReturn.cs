using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemReturn : MonoBehaviour
{

    public Transform slot;
    public MeshRenderer slotRenderer;
    private OVRGrabbable objGrabbed;

    // Start is called before the first frame update
    void Start()
    {
        objGrabbed = GetComponent<OVRGrabbable>();
    }

    void Update()
    {
        //checks to see if player is holding
        if (objGrabbed.isGrabbed == true)
        {
            slotRenderer.enabled = true;
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Restricted")
        {
            StartCoroutine(ObjectReturn());
        }
    }

    //method for delay (or could just use Invoke)
    IEnumerator ObjectReturn()
    {
        yield return new WaitForSeconds(1);
        slotRenderer.enabled = false;
        gameObject.transform.position = slot.transform.position;
        gameObject.transform.rotation = slot.transform.rotation;
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
    }


}
