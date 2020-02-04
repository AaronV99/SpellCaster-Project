using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeTriggerDetect : MonoBehaviour
{
    ////TESTING VARIABLES
    //public Material mat1, mat2;    
    ////
    public bool nodeTriggered,nodeStarted, nodeClear = true;
    public GameObject LineRendererRef;
    public int nodeID;
    void Start()
    {

        for (int i = 0; i < gameObject.GetComponentInParent<Casting>().node.Length; i++)
        {
            if(gameObject.GetComponentInParent<Casting>().node[i].name == gameObject.name && nodeStarted == false)
            {
                nodeStarted = true;
                nodeID = i;
            }
        }
    }

    void Update()
    {
       

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finger" && other.GetComponentInParent<CastingHand>().isCasting)
        {
            ////Testing stuff---------------------------------------------------
            //gameObject.GetComponent<MeshRenderer>().material = mat2;
            ////Testing stuff---------------------------------------------------

            //Setting Line Renderer new position for vertex
            if (LineRendererRef.GetComponent<LineRenderer>().positionCount <= 0)
            {
                LineRendererRef.GetComponent<LineRenderer>().positionCount++;
                LineRendererRef.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                LineRendererRef.GetComponent<DrawLine>().origin = transform;
            }else if(LineRendererRef.GetComponent<LineRenderer>().positionCount > 0)
            {
                LineRendererRef.GetComponent<LineRenderer>().positionCount++;
                LineRendererRef.GetComponent<LineRenderer>().SetPosition(LineRendererRef.GetComponent<LineRenderer>().positionCount - 1, transform.position);
                LineRendererRef.GetComponent<DrawLine>().destination = transform;
            }

            if (!gameObject.GetComponentInParent<Casting>().sequence.Contains(nodeID))
            {
                gameObject.GetComponentInParent<Casting>().AddToSequence(gameObject.name);

            }
            nodeClear = false;
        
        }
    }

    public void NodeReset()
    {
        if (nodeClear == false && nodeTriggered == true)
        {
            ////Testing stuff---------------------------------------------------
            //gameObject.GetComponent<MeshRenderer>().material = mat1;
            ////Testing stuff---------------------------------------------------

            gameObject.GetComponentInParent<Casting>().CheckSequence(nodeID);
            nodeTriggered = false;
            gameObject.GetComponentInParent<Casting>().sequence.Clear();
            nodeClear = true;
        }
    }
}


