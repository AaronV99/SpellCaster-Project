using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class spawnManagerV3 : MonoBehaviour
{

    //*******
    //Can set if a boss should spawn at a wave or not and when it should spawn (after x enemies remaining)
    //Defeat X number of enemies to continue waves
    //Total waves can be set manually in inspector
    //Total enemies and enemy type per wave can be set manually in inspector
    //*******

    //number of waves
    public Wave[] waves;

    //Spawn points
    public Transform[] spawnPoints;

    //Text
    public Text waveNumberText;
    public Text enemiesRemainingText;
    public Text waveCountdownText;


    public float timeBetweenWaves = 5f;
    private float countdown = 5f;

    //Integars
    [SerializeField] public static int EnemiesAlive = 0;
    public int enemiesSpawned = 0;
    private int waveIndex = 0;
    int randomSpawnPoint;
    int maxEnemies;

    //Gameobjects
    public GameObject gate;
    public GameObject startCanvas;
    //public GameObject nextCanvas;

    //Game states
    enum State { Start, Wave, Intermission, Defended }
    [SerializeField] State currentState;


    void Start()
    {
        currentState = State.Start;
        waveCountdownText.enabled = false;
    }

    void Update()
    {
        //Updating the amount of enemies left
        EnemyCounter();

        switch (currentState)
        {
            case State.Start:
                //Empty state- so that game doesnt start on wave or intermission state
                break;
            case State.Wave:
                CheckEnemyAlive();
                break;
            case State.Intermission:
                WaveCheck();
                break;
            case State.Defended:
                //Empty state- so that game doesnt finish on wave or intermission state
                break;
            default:
                Debug.Log("Test");
                break;
        }
    }

    //Logic 1 of wave system
    IEnumerator SpawnWave()
    {
        Wave wave = waves[waveIndex];

        //Wave Counter
        waveNumberText.text = "Wave: " + (waveIndex + 1).ToString() + " / " + waves.Length.ToString();

        //update counter if boss spawn enabled
        if (wave.SpawnBoss)
        {
            EnemiesAlive = wave.count + 1;
        }
        else
        {
            EnemiesAlive = wave.count;
        }

        //next wave
        waveIndex++;

        //Loop through Wave Class enemy list and call the spawn function every x random seconds
        for (int i = 0; i < wave.count; i++)
        {
            maxEnemies = (maxEnemies >= wave.enemies.Count - 1) ? 0 : maxEnemies + 1;
            SpawnEnemy(wave.enemies[maxEnemies]);
            yield return new WaitForSeconds(Random.Range(3, 10));
        }
    }

    //Spawning the enemies
    void SpawnEnemy(GameObject enemy)
    {
        //Pick a random spawn point in the spawnpoint array and instantiation an enemy
        randomSpawnPoint = Random.Range(0, spawnPoints.Length);
        Instantiate(enemy, spawnPoints[randomSpawnPoint].position, spawnPoints[randomSpawnPoint].rotation);
        //increment int every time an enemy is spawned
        enemiesSpawned++;
    }

    //Put enemy amount to the text string
    public void EnemyCounter()
    {
        if (EnemiesAlive >= 0)
        {
            enemiesRemainingText.text = EnemiesAlive.ToString();
        }
    }

    //Logic 2 of wave system
    //Check if enemies are still alive and if so continue to spawn them otherwise change state to intermission 
    void CheckEnemyAlive()
    {
        if (EnemiesAlive > 0)
        {
            //if the gate is destroyed - Game over - stop spawning enemies
            if (gate == null)
            {
                this.enabled = false;
                StopAllCoroutines();
            }
            //check if enemiesalive is less than or equals to boss spawn variable and boss spawned bool
            else if ((waves[waveIndex - 1].SpawnBoss) && (EnemiesAlive <= waves[waveIndex - 1].EnemiesLeftBeforeBossSpawn + 1) && (!waves[waveIndex - 1].BossSpawned))
            {
                if (waves[waveIndex - 1].Boss == null)
                {
                    Debug.LogError("The boss GameObject must be assigned via Inspector!");
                }
                else
                {
                    //set boss spawn to true and spawn the boss at random spawn location
                    waves[waveIndex - 1].BossSpawned = true;
                    Instantiate(waves[waveIndex - 1].Boss, spawnPoints[randomSpawnPoint].position, spawnPoints[randomSpawnPoint].rotation);
                }
            }
            return;
        }
        else
        {
            currentState = State.Intermission;
        }
    }

    //Check if there are still waves remaining 
    void WaveCheck()
    {
        if (waveIndex < waves.Length)
        {
            if (countdown <= 0f)
            {
                StartCoroutine(SpawnWave());
                currentState = State.Wave;
                countdown = timeBetweenWaves;
                enemiesSpawned = 0;
                waveCountdownText.enabled = false;
                //play sound indicator here

                return;
            }

            waveCountdownText.enabled = true;

            countdown -= Time.deltaTime;

            countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

            waveCountdownText.text = countdown.ToString("f0");

            //nextCanvas.SetActive(true);
        }
        else
        {
            currentState = State.Defended;

            Debug.Log("Completed");

            this.enabled = false;
        }
    }

    //Button functions
    //Set current state to wave and spawn wave
    public void StartWave()
    {
        currentState = State.Wave;
        enemiesSpawned = 0;
        StartCoroutine(SpawnWave());
        startCanvas.SetActive(false);
    }

    //public void NextWave()
    //{
    //    currentState = State.Wave;
    //    enemiesSpawned = 0;
    //    StartCoroutine(SpawnWave());
    //    nextCanvas.SetActive(false);
    //}

}
