using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Wave {

    [Tooltip("List of enemies to be spawned")]
    public List<GameObject> enemies = new List<GameObject>();

    [Tooltip("Number of enemies")]
    public int count;

    [Tooltip("Should a boss be spawned?")]
    public bool SpawnBoss = false;

    [Tooltip("Boss of the wave")]
    public GameObject Boss;

    [Tooltip("Number of enemies left before starting to spawn the Boss")]
    public int EnemiesLeftBeforeBossSpawn = 0;

    [HideInInspector]
    public bool BossSpawned = false;

}
