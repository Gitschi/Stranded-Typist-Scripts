using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillEnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject flamePrefab;

    public GameObject enemyPrefab;
    public GameObject[] spawnPoints;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("SpawnWave", 0f, 2f);

        // Instantiates effect
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(flamePrefab, spawnPoints[i].transform);
        }

    }

    // Spawns waves in drill mode
    public void SpawnWave()
    {
        int posPick = Random.Range(0, spawnPoints.Length);


        // Only executed if spawnpoints exist
        if (spawnPoints[0])
        {
            Instantiate(enemyPrefab, spawnPoints[posPick].transform.position, Quaternion.identity);
        }
    }
}
