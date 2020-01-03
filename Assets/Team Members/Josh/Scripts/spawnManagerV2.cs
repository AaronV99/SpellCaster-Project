using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class spawnManagerV2 : MonoBehaviour
{
    //The list of spawn points ;
    public Transform[] spawnPoints;

    // The list of enemies prefabs stored 
    public GameObject[] enemy, bosses;
    public bool randomizeBossArray;
    public List<GameObject> bossesNew = new List<GameObject>();
    public List<GameObject> enemyNew = new List<GameObject>();

    public int curBossIndex;
    public int enemyLimitForNextWave = 5;
    public int enemiesPerWave;

    //to store random enemy at runtime to instantiate
    private GameObject tempEnemy, tempBoss;


    public GameObject startCanvas, nextWaveCanvas, restartCanvas;

    public GameObject objectivePoint;


    bool isSpawning;


    float diffrence = 0;


    float spawnStartTime;


    public float nextEnemyDelay = 1;


    int waveNumber;


    int randomSpawnPoint;

    public Text remainingEnemiesText;


    public Text currentWave;


    enum State { Wave, Boss, Intermission, Defended }
    [SerializeField] State currentState;

    void Start()
    {

        System.Random rand = new System.Random();
        foreach (GameObject go in bosses)
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
        enemyCounter();
        //States
        switch (currentState)
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

    int leftEnemiesCount()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (allEnemies.Length >= 0) {
            return allEnemies.Length + 1;
        }
        else
        {
            return 0;
        }
    }

    public bool doneSpawning;
    bool bossSpawned = false;

    void SpawnWave()
    {
        if (isSpawning)
        {

            //set current wave to text
            currentWave.text = "Wave: " + (waveNumber + 1).ToString();

            
            if (enemyCount <= enemyLimitForNextWave)
            {

                if (!bossSpawned)
                {
                    spawnBoss();
                    bossSpawned = true;
                }
                //Stop the spawning
            if(enemyCount ==0)
            {
                isSpawning = false;

                //increase the wave to 1
                waveNumber++;

                //boss state
                currentState = State.Boss;
              //  spawnBoss();
                StopAllCoroutines();
                doneSpawning = false;
                bossSpawned = false;
            }
            }
        }
    }

    int enemiesSpawned = 0;

    IEnumerator spawnEnemies(List<GameObject> temp, int enemyCount)
    {
        int maxCount = enemyCount;
        
        for (int curCount = 0; curCount < maxCount; curCount++)
        {

            foreach (GameObject t in temp)
            {
                //store a random spawn point within that array list
                randomSpawnPoint = UnityEngine.Random.Range(0, spawnPoints.Length);
                // Create an instance of the enemy prefab at any of the spawn points randomly
                GameObject temp1 = Instantiate(t, spawnPoints[randomSpawnPoint].position, spawnPoints[randomSpawnPoint].rotation);
                temp1.GetComponent<Agent>().goal = objectivePoint.transform;
                //range given it the function to set the spawn time of each enemy after one enemy is spawned
                //to increase difficulty, range can be decreased e.g (0f,3f) enemy will spawn from after 0 seconds till 3f randomly
                nextEnemyDelay += (UnityEngine.Random.Range(1f, 5f)) + diffrence;
                enemiesSpawned++;
                yield return new WaitForSeconds(UnityEngine.Random.Range(1f,5f));
            }
        }
        doneSpawning = true;
        StopAllCoroutines();
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

    public int maxEnemies;
    public int enemyCount;

    public void enemyCounter()
    {
        if(leftEnemiesCount() > 0)
        {
            enemyCount = maxEnemies - (enemiesSpawned - leftEnemiesCount())-1;
            remainingEnemiesText.text = enemyCount.ToString();
        }
    }

    public void StartWave()
    {
        List<GameObject> enemyList = new List<GameObject>();
        //getting random enemy 
        tempEnemy = enemy[waveNumber];

        enemyList.Add(tempEnemy);

        foreach (GameObject g in enemyNew)
        {
            enemyList.Add(g);
        }
        maxEnemies = enemyList.Count* enemiesPerWave;
        enemiesSpawned = 0;
        StartCoroutine(spawnEnemies(enemyList, enemiesPerWave));

        enemyNew.Add(tempEnemy);
        //change to wave state
        currentState = State.Wave;

        //Initiate waveNumber to 0
        waveNumber = 0;

        //Set isSpawning to true
        isSpawning = true;

        startCanvas.SetActive(false);
    }

    //Function for button when clicked
    public void nextWave()
    {
        List<GameObject> enemyList = new List<GameObject>();
        //getting random enemy 
        tempEnemy = enemy[waveNumber];

        enemyList.Add(tempEnemy);

        foreach (GameObject g in enemyNew)
        {
            enemyList.Add(g);
        }
        maxEnemies = enemyList.Count*enemiesPerWave;
        enemiesSpawned = 0;
        StartCoroutine(spawnEnemies(enemyList, enemiesPerWave));

        enemyNew.Add(tempEnemy);
        //change to wave state
        currentState = State.Wave;
        //spawn next wave
        isSpawning = true;
        //reset difference
        diffrence = 0;
        //disables the canvas on press
        nextWaveCanvas.SetActive(false);
    }

    //Function to restart game
    public void restart()
    {
        PlayerPrefs.DeleteAll();
        //Restart game
        SceneManager.LoadScene("DefenceOne");

    }
 }


