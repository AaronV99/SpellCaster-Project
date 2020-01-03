using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class spawnManagerV1 : MonoBehaviour
{
    //The list of spawn points ;
    public Transform[] spawnPoints;

    // The list of enemies prefabs stored 
    public GameObject[] enemy, bosses;
    public bool randomizeBossArray;
    public List<GameObject> bossesNew = new List<GameObject>();
    public int curBossIndex;
    //to store random enemy at runtime to instantiate
    private GameObject tempEnemy, tempBoss;

    //Get the button to diable/enable it
    public GameObject startCanvas, nextWaveCanvas, restartCanvas;

    //Agent point b location;
    public GameObject objectivePoint;

    //bool to check whether wave is currently spwaning or not
    bool isSpawning;

    //variable to store difference
    float diffrence = 0;

    //Getting time in seconds for single spawn 
    float spawnStartTime;

    //Average time randomly generated to spawn next enemy
    float nextEnemy;

    //Current time during wave
    float currentTime = 0;

    // Wave Number that is to be spawned
    int waveNumber;

    //store spoawn point in this variable
    int randomSpawnPoint;

    //store text to display remaining wave time
    public Text remainingTimeText;

    //store text to display current wave;
    public Text currentWave;

    //float to add additional X seconds to each wave
    [SerializeField]
    float increasedWaveTimer = 20f;

    //float to set first wave timer
    [SerializeField]
    float waveOneStartTimer = 30f;

    enum State {Wave, Boss, Intermission, Defended }
    [SerializeField]State currentState;

    

    void Start()
    {
        
        System.Random rand = new System.Random();
        foreach(GameObject go in bosses)
        {
            bossesNew.Add(go);
        }
        if (randomizeBossArray)
        {
            for (int i = 0; i < bossesNew.Count - 2; i++)
            {
                int j = rand.Next(i, bossesNew.Count - 1);

                GameObject temp = bossesNew[i];
                bossesNew[i] = bossesNew[j];
                bossesNew[j] = temp;
            }
        }
    }

    void Update()
    {
        //States
        switch(currentState)
        {
            case State.Wave:
                SpawnWave();
                break;
            case State.Intermission:
                nextLevelCanvas();
                break;
            case State.Boss:
                isEnemyAlive();
                break;
            case State.Defended:
                nextLevelCanvas();
                break;
            default:
                Debug.Log("Test");
                break;
        }
    }

    void SpawnWave()
    {
        if (isSpawning)
        {
            //store current time to compare
            currentTime = getSeconds();

            if (currentTime - spawnStartTime < (((waveNumber * increasedWaveTimer) + waveOneStartTimer + diffrence)))
            {
                //Setting the remaining time text
                remainingTimeText.text = "Defend for: " + (((waveNumber * increasedWaveTimer) + waveOneStartTimer + diffrence) - (currentTime - spawnStartTime)).ToString();

                //set current wave to text
                currentWave.text = "Wave: " + (waveNumber + 1).ToString();

                //getting random enemy 
                tempEnemy = enemy[UnityEngine.Random.Range(0, waveNumber + 1)];

                //condition to spawn next enemy after random seconds have passed
                if (currentTime - spawnStartTime >= nextEnemy)
                {
                    //store a random spawn point within that array list
                    randomSpawnPoint = UnityEngine.Random.Range(0, spawnPoints.Length);

                    // Create an instance of the enemy prefab at any of the spawn points randomly
                    Instantiate(tempEnemy, spawnPoints[randomSpawnPoint].position, spawnPoints[randomSpawnPoint].rotation);

                    //range given it the function to set the spawn time of each enemy after one enemy is spawned
                    //to increase difficulty range can be decreased e.g (0f,3f) enemy will spawn from after 0 seconds till 5f randomly
                    nextEnemy += (UnityEngine.Random.Range(1f, 5f)) + diffrence;
                }
                //make random instantiatied enemy move to objective point
                tempEnemy.GetComponent<Agent>().goal = objectivePoint.transform;
            }
            else
            {
                
                //store all objects with enemy tag in an array
                GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

                //destroy all objects within that array
                foreach (GameObject e in allEnemies)
                    Destroy(e);

                //Clear text
                remainingTimeText.text = "";

                //Stop the spawning
                isSpawning = false;

                //increase the wave to 1
                waveNumber++;

                //boss state
                currentState = State.Boss;
                spawnBoss();
            }
        }
    }

    void spawnBoss()
    {
        //store random boss from array list, and a random spawn location and instantiate one
        tempBoss = bossesNew[curBossIndex];
        randomSpawnPoint = UnityEngine.Random.Range(0, spawnPoints.Length);
        GameObject go = Instantiate(tempBoss, spawnPoints[randomSpawnPoint].position, spawnPoints[randomSpawnPoint].rotation);
        go.GetComponent<Agent>().goal = objectivePoint.transform;

        if (curBossIndex < bossesNew.Count - 1)
        {
            curBossIndex++;
        }
        else
        {
            curBossIndex = 0;
        }
    }

    
    //bool function to check if boss is alive
    bool isEnemyAlive()
    {
        if (GameObject.FindGameObjectWithTag("Boss") == null)
        {
            //if boss is dead, change state to intermission
            currentState = State.Intermission;
            return false;
        }
        return true;
    }

    void nextLevelCanvas()
    {
        //condition to check whether waveNumber does not exceed enemy array list
        if (waveNumber < enemy.Length)
        {
            nextWaveCanvas.SetActive(true);
        }
        else
        {
            currentState = State.Defended;
            restartCanvas.SetActive(true);
        }
    }

    //Function for getting current time
    float getSeconds()
    {
        return float.Parse(DateTime.Now.Second.ToString()) + (float.Parse(DateTime.Now.Hour.ToString()) * 60 * 60) + (float.Parse(DateTime.Now.Minute.ToString()) * 60);
    }

    //Function for button when clicked
    public void StartWave()
    {
        //change to wave state
        currentState = State.Wave;

        //Initiate waveNumber to 0
        waveNumber = 0;

        //Set isSpawning to true
        isSpawning = true;

        //get time in seconds
        spawnStartTime = getSeconds();

        //spawn next enemy after 2 seconds of wave 
        nextEnemy = 2;

        startCanvas.SetActive(false);
    }

    //Function for button when clicked
    public void nextWave()
    {
        //change to wave state
        currentState = State.Wave;
        //spawn next wave
        isSpawning = true;
        //reset the timer
        spawnStartTime = getSeconds();
        //reset difference
        diffrence = 0;
        //disables the canvas on press
        nextWaveCanvas.SetActive(false);
        //set nextEnemy to X so after starting new wave first enemy will appear after X seconds
        nextEnemy = 2;
    }

    //Function to restart game
    public void restart()
    {
        PlayerPrefs.DeleteAll();
        //Restart game
        SceneManager.LoadScene("BoxTestV1");

    }
}
