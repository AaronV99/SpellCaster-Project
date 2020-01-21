using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casting : MonoBehaviour
{
    public GameObject[] node;
    public List<int> sequence = new List<int>();

    //Testing Variables
    int sequenceCompletion = 0;
    public List<int> summonCubeSpell = new List<int>();
    public GameObject testingCube;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //TestingPurposes

        //TestingPurposes
    }

    public void AddToSequence(string nodeName)
    {

        for (int i = 0; i < node.Length; i++)
        {
            if (node[i].name == nodeName)
            {
                sequence.Add(i);
                node[i].gameObject.GetComponent<NodeTriggerDetect>().nodeTriggered = true;
            }
        }

    }

    public void CheckSequence(int nodeID)
    {
        for(int i = 0; i < sequence.Count; i++)
        {
            if (sequence.Contains(nodeID) && summonCubeSpell.Contains(nodeID))
            {
                sequenceCompletion++;

                //Testing Purposes
                if(sequenceCompletion == summonCubeSpell.Count)
                {
                    CastSpell("SpawnCube");
                }
                //Testing Purposes
            }
        }
        sequenceCompletion = 0;

    }

    public void CastSpell(string spellName)
    {
        if(spellName == "SpawnCube")
        {
            Instantiate(testingCube, gameObject.transform.position, Quaternion.identity);
        
        }

    
    }
    //Note to self: Currently only works with the one spell. Need to make it more dynamic so that it can detect which spell is being cast; Possibly look into Jagged Arrays (an Array of Arrays);
}
