using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class Casting : MonoBehaviour
{
    public GameObject[] node;
    public List<int> sequence = new List<int>();
    bool cast;
    public GameObject self, castingDummy;
    GameObject spell;

    //Testing Variables
    public int cubeCompletion, fireBallCompletion, iceSpikeCompletion, earthWallCompletion;
    public List<int> summonCubeSpell = new List<int>();
    public List<int> fireBall = new List<int>();
    public List<int> iceSpike = new List<int>();
    public List<int> earthWall = new List<int>();

    public GameObject testingCube, fireBallPrefab, earthWallPrefab, iceSpikePrefab;

    //End of Testing Variables;

   
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
            //if (sequence.Contains(nodeID) && summonCubeSpell.Contains(nodeID)) //Testing spell 0
            //{
            //    cubeCompletion++;
            //}

            if (sequence.Contains(nodeID) && fireBall.Contains(nodeID)) //Fireball
            {
                fireBallCompletion++;
            }

            if (sequence.Contains(nodeID) && iceSpike.Contains(nodeID)) //Ice Spike
            {
                iceSpikeCompletion++;
            }

            if (sequence.Contains(nodeID) && earthWall.Contains(nodeID)) //Earth Wall
            {
                earthWallCompletion++;
            }
        }
        CheckCompletion();
       
    }

    public void CheckCompletion()
    {
        //Testing Purposes
        if (cubeCompletion > fireBallCompletion && cubeCompletion > iceSpikeCompletion && cubeCompletion > earthWallCompletion && !cast)
        {
            float n = 0.7f * summonCubeSpell.Count;

            if (cubeCompletion >= n)
            {
                CastSpell("SpawnCube");
                cast = true;
            }//Testing Purposes
        }        
        else if (fireBallCompletion > cubeCompletion && fireBallCompletion > iceSpikeCompletion && fireBallCompletion > earthWallCompletion && !cast)
        {
            float n = 0.7f * fireBall.Count;
            if (fireBallCompletion >= n)
            {
                CastSpell("FireBall");
                cast = true;
            }
        }
        else if (iceSpikeCompletion > cubeCompletion && iceSpikeCompletion > fireBallCompletion && iceSpikeCompletion > earthWallCompletion && !cast)
        {
            float n = 0.7f * iceSpike.Count;
            if (iceSpikeCompletion >= n)
            {
                CastSpell("IceSpike");
                cast = true;
            }
        }
        else if (earthWallCompletion > cubeCompletion && earthWallCompletion > fireBallCompletion && earthWallCompletion > iceSpikeCompletion && !cast)
        {
            float n = 0.7f * earthWall.Count;
            if (earthWallCompletion >= n)
            {
                CastSpell("EarthWall");
                cast = true;
            }
        }

    }
    public void CastSpell(string spellName)
    {
        //Testing
        //if(spellName == "SpawnCube")
        //{
            
        //    Instantiate(testingCube, gameObject.transform.position, selfRotation);
        //    print("Spawned a cube");
        //}
        //Testing

        if(spellName == "FireBall")
        {
            spell = Instantiate(fireBallPrefab, castingDummy.transform.position, castingDummy.transform.rotation) as GameObject;
            print("Spawned a FireBall");
        }

        if (spellName == "EarthWall")
        {
            spell = Instantiate(earthWallPrefab, castingDummy.transform.position, castingDummy.transform.rotation) as GameObject;
            print("Spawned a EarthWall");
        }

        if (spellName == "IceSpike")
        {
            spell = Instantiate(iceSpikePrefab, castingDummy.transform.position, castingDummy.transform.rotation) as GameObject;
            print("Spawned a IceSpike");
        }

    }
}