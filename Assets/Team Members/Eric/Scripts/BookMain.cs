using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookMain : MonoBehaviour
{
    public GameObject self, book, dummy;
    public bool isGrabbingBook;
    bool touchingBook, scaleGateUp;
    Vector3 scaleStart = new Vector3(0.3f, 0.3f, 0.3f), scaleTarget = new Vector3(0.5f, 0.5f, 0.5f);

    float gripPress, growthSpeed = 1;
    int HandID;

    // Start is called before the first frame update
    void Start()
    {
        //Sets and Id for each hand to distinguish easily in code
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
        switch (HandID)
        {
            case 0:
                gripPress = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch);


                break;

            case 1:
                gripPress = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);


                break;

            default:
                print("No hand available");

                break;
        }

        if (!isGrabbingBook && touchingBook && gripPress >= 0.5)
        {
            GrabBook();
        } else if(isGrabbingBook && gripPress <= 0.5)
        {
            ReleaseBook(book);
        }

        //Scaling of book
        if (scaleGateUp)
        {
            if (book.transform.localScale.x < scaleTarget.x && book.transform.localScale.z < scaleTarget.z && book.transform.localScale.y < scaleTarget.y)
            {
                book.transform.localScale += new Vector3(growthSpeed, growthSpeed, growthSpeed) * Time.deltaTime;

            }
            else if (book.transform.localScale.x > scaleTarget.x && book.transform.localScale.z > scaleTarget.z && book.transform.localScale.y > scaleTarget.y)
            {
                scaleGateUp = false;
            }

        }

        //Error checking for book grab
        if(gripPress <= 0.5 && book != null)
        {
            foreach(Transform child in self.transform)
            {
                if(child.tag == "Book")
                {
                    ReleaseBook(child.gameObject);
                }
            }
        }
    }
    void GrabBook()
    {
        if (self.gameObject.GetComponent<StaffMain>().isGrabbingStaff != true)
        {
            isGrabbingBook = true;
            scaleGateUp = true;
            book.transform.SetParent(self.transform);
            book.transform.position = self.transform.position;
            print("grabbing book");
            if (self.tag == "HandR")
            {
                book.transform.localEulerAngles = new Vector3(self.transform.rotation.x, self.transform.rotation.y, 90);

            }
            else if (self.tag == "HandL")
            {
                book.transform.localEulerAngles = new Vector3(self.transform.rotation.x, self.transform.rotation.y, -90);

            }
        }
    }

    void ReleaseBook(GameObject bookObj)
    {
        if(book != null)
        {
            bookObj.transform.localScale = scaleStart;
            scaleGateUp = false;
            book = null;
            bookObj.transform.SetParent(dummy.transform);
            bookObj.transform.position = dummy.transform.position;
            bookObj.transform.localEulerAngles = new Vector3(21, 0, -11);
            isGrabbingBook = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Book")
        {
            touchingBook = true;
            book = GameObject.FindGameObjectWithTag("Book");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Book")
        {
            touchingBook = false;
        }
    }
}
