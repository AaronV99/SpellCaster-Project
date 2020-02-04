using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casting : MonoBehaviour
{
    public GameObject[] node;
    public List<int> sequence = new List<int>();
    bool cast;

    //Testing Variables
    public int cubeCompletion, testOneCompletion, testTwoCompletion, testThreeCompletion;
    public List<int> summonCubeSpell = new List<int>();
    public List<int> testOne = new List<int>();
    public List<int> testTwo = new List<int>();
    public List<int> testThree = new List<int>();

    public GameObject testingCube, testingOne, testingTwo, testingThree;

    //End of Testing Variables;

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
            //Testing spell 0
            if (sequence.Contains(nodeID) && summonCubeSpell.Contains(nodeID)) //Testing spell 0
            {
                cubeCompletion++; 
            }
            
            if (sequence.Contains(nodeID) && testOne.Contains(nodeID)) //Testing spell 1
            {
                testOneCompletion++;
            }

            if (sequence.Contains(nodeID) && testTwo.Contains(nodeID)) //Testing spell 2
            {
                testTwoCompletion++;
            }

            if (sequence.Contains(nodeID) && testThree.Contains(nodeID)) //Testing spell 3
            {
                testThreeCompletion++;
            }
        }
        CheckCompletion();
       
    }

    public void CheckCompletion()
    {
        //Testing Purposes
        if (cubeCompletion > testOneCompletion && cubeCompletion > testTwoCompletion && cubeCompletion > testThreeCompletion && !cast)
        {
            float n = 0.7f * summonCubeSpell.Count;

            if (cubeCompletion >= n)
            {
                CastSpell("SpawnCube");
                cast = true;
            }
        }
        else if (testOneCompletion > cubeCompletion && testOneCompletion > testTwoCompletion && testOneCompletion > testThreeCompletion && !cast)
        {
            float n = 0.7f * testOne.Count;
            if (testOneCompletion >= n)
            {
                CastSpell("TestOne");
                cast = true;
            }
        }
        else if (testTwoCompletion > cubeCompletion && testTwoCompletion > testOneCompletion && testTwoCompletion > testThreeCompletion && !cast)
        {
            float n = 0.7f * testTwo.Count;
            if (testTwoCompletion >= n)
            {
                CastSpell("TestTwo");
                cast = true;
            }
        }
        else if (testThreeCompletion > cubeCompletion && testThreeCompletion > testOneCompletion && testThreeCompletion > testTwoCompletion && !cast)
        {
            float n = 0.7f * testThree.Count;
            if (testThreeCompletion >= n)
            {
                CastSpell("TestThree");
                cast = true;
            }
        }
        //Testing Purposes

    }
    public void CastSpell(string spellName)
    {
        if(spellName == "SpawnCube")
        {
            Instantiate(testingCube, gameObject.transform.position, Quaternion.identity);
            print("Spawned a cube");
        }

        if(spellName == "TestOne")
        {
            Instantiate(testingOne, gameObject.transform.position, Quaternion.identity);
            print("Spawned a One");
        }

        if (spellName == "TestTwo")
        {
            Instantiate(testingTwo, gameObject.transform.position, Quaternion.identity);
            print("Spawned a Two");
        }

        if (spellName == "TestThree")
        {
            Instantiate(testingThree, gameObject.transform.position, Quaternion.identity);
            print("Spawned a Three");
        }

    }
}
