using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreSpawnHandler : MonoBehaviour {

    [SerializeField] private GameObject[] spawnBatchPoints;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform enemyHolder;

    private GameManager GM;
    private PlayerBehaviour playerBehaviour;
    private int currentBatch;
    private List<PreSpawn> preSpawns = new List<PreSpawn>();

    public int totalDefeated;
    public int totalSpawned;

    private void Awake()
    {
        GM = FindObjectOfType<GameManager>();
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();
    }

    void Start () {
        AddPrespawns();
    }

    // Instantiates batches of enemies
    public void Spawn()
    {
        for(int i = 0; i < preSpawns[0].spawnPoints.Count; i++)
        {
            GameObject go = Instantiate(preSpawns[0].enemyPrefab, preSpawns[0].spawnPoints[i].transform.position, preSpawns[0].spawnPoints[i].transform.rotation);
            go.transform.parent = enemyHolder;

            totalSpawned++;
        }
        preSpawns.Remove(preSpawns[0]);
    }

    // Adds preSpawn points to list
    private void AddPrespawns()
    {
        PreSpawn[] createdPreSpawns =
        {
            new PreSpawn(spawnBatchPoints[0], spawnPoints, 0, 1, enemyPrefabs[1]), // Batch 1
            new PreSpawn(spawnBatchPoints[1], spawnPoints, 2, 2, enemyPrefabs[2]), // Batch 2
            new PreSpawn(spawnBatchPoints[2], spawnPoints, 3, 3, enemyPrefabs[2]), // Batch 3
            new PreSpawn(spawnBatchPoints[3], spawnPoints, 4, 6, enemyPrefabs[0]), // Batch 4
            new PreSpawn(spawnBatchPoints[4], spawnPoints, 7, 8, enemyPrefabs[0]), // Batch 5
            new PreSpawn(spawnBatchPoints[5], spawnPoints, 9, 9, enemyPrefabs[2]), // Batch 6
            new PreSpawn(spawnBatchPoints[6], spawnPoints, 10, 11, enemyPrefabs[1]), // Batch 7
            new PreSpawn(spawnBatchPoints[7], spawnPoints, 12, 13, enemyPrefabs[0]) // Batch 8
        };

        for (int i = 0; i < createdPreSpawns.Length; i++)
        {
            preSpawns.Add(createdPreSpawns[i]);
        }
    }

    // Increments number of defeated pre spawned enemies
    // !! Different from defeated on gamemanager !!
    public void IncrementDefeated()
    {
        totalDefeated++;

        TrySetOnRails();

        // Tries again in case it was triggered before reaching the checkpoint
        if(playerBehaviour.gameState == GameStates.OnRail)
        {
            Invoke("TrySetOnRails", 2f);
        }
    }

    // Try to set on rails
    public void TrySetOnRails()
    {
        if(totalDefeated >= totalSpawned)
        {
            GM.SetOnRails();
        }
    }
}
