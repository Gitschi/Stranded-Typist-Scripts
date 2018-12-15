using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillModeManager : MonoBehaviour {

    private GameManager GM;
    private Audiomanager AM;
    [SerializeField] private GameObject flamePrefab;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] enemyPrefabs;
    private List<DrillWave> drillWaves = new List<DrillWave>();
    private List<GameObject> playerPositions = new List<GameObject>();
    private List<GameObject> spawnPoints = new List<GameObject>();

    private int levelPick;
    private int speedModeAmount;

    public bool speedSpawnPossible;

    private void Awake()
    {
        GM = FindObjectOfType<GameManager>();
        AM = FindObjectOfType<Audiomanager>();
        spawnPoints.AddRange(GameObject.FindGameObjectsWithTag("SpawnPoint"));
        playerPositions.AddRange(GameObject.FindGameObjectsWithTag("Waypoint"));

        // Sorts spawnpoints by name
        if (spawnPoints.Count > 0)
        {
            spawnPoints.Sort(delegate (GameObject a, GameObject b)
            {
                return (a.name).CompareTo(b.name);
            });
        }

        // Sort playerPositions by name
        if (playerPositions.Count > 0)
        {
            playerPositions.Sort(delegate (GameObject a, GameObject b)
            {
                return (a.name).CompareTo(b.name);
            });
        }
    }

    private void Start()
    {
        speedSpawnPossible = false;
        speedModeAmount = 30;

        SetPickedLevel();
        AddWaves();
        SetStartPosition();

        if(GM.gameMode == GameMode.Speed)
        {
            speedSpawnPossible = true;
        }
        if (GM.gameMode == GameMode.Miss)
        {
            StartCoroutine("MissSpawn");
        }
    }

    private void Update()
    {
        if (speedSpawnPossible && !GM.gameIsOver)
        {
            if(drillWaves[levelPick].amountSpawned < speedModeAmount)
            {
                int posPick = Random.Range(0, drillWaves[levelPick].spawnPoints.pointList.Count);
                GM.totalSpawned++;

                // Instantiates effect
                for (int i = 0; i < drillWaves[levelPick].spawnPoints.pointList.Count; i++)
                {
                    // Checks if child count is 0 to only spawn the effect once
                    if (drillWaves[levelPick].spawnPoints.pointList[i].transform.childCount == 0)
                    {
                        Instantiate(flamePrefab, drillWaves[levelPick].spawnPoints.pointList[i].transform);
                    }
                }

                // Instantiates picked enemy at picked position using the parents rotation
                Instantiate(drillWaves[levelPick].enemyPrefab, drillWaves[levelPick].spawnPoints.pointList[posPick].transform.position, drillWaves[levelPick].spawnPoints.pointList[posPick].transform.rotation);
                drillWaves[levelPick].amountSpawned++;

                speedSpawnPossible = false;
            }
            else
            {
                GM.SetGameOver();
            }
        }
    }

    // Creates waves and adds them to list
    private void AddWaves()
    {
        DrillWave[] createdWaves =
        {
            new DrillWave(playerPositions[0], enemyPrefabs[0], new SpawnBatch(spawnPoints, 0, 1)),    // PlayerPos 1
            new DrillWave(playerPositions[1], enemyPrefabs[1], new SpawnBatch(spawnPoints, 2, 2)),    // PlayerPos 2
            new DrillWave(playerPositions[2], enemyPrefabs[0], new SpawnBatch(spawnPoints, 3, 4)),    // PlayerPos 3
            new DrillWave(playerPositions[3], enemyPrefabs[1], new SpawnBatch(spawnPoints, 5, 6)),    // PlayerPos 4
            new DrillWave(playerPositions[4], enemyPrefabs[0], new SpawnBatch(spawnPoints, 7, 7)),    // PlayerPos 5
            new DrillWave(playerPositions[5], enemyPrefabs[0], new SpawnBatch(spawnPoints, 8, 9)),    // PlayerPos 6
            new DrillWave(playerPositions[6], enemyPrefabs[1], new SpawnBatch(spawnPoints, 10, 12)),  // PlayerPos 7
            new DrillWave(playerPositions[7], enemyPrefabs[0], new SpawnBatch(spawnPoints, 13, 13)),  // PlayerPos 8
            new DrillWave(playerPositions[8], enemyPrefabs[0], new SpawnBatch(spawnPoints, 14, 15)),  // PlayerPos 9
            new DrillWave(playerPositions[9], enemyPrefabs[0], new SpawnBatch(spawnPoints, 16, 16)),  // PlayerPos 10
            new DrillWave(playerPositions[10], enemyPrefabs[0], new SpawnBatch(spawnPoints, 17, 17)), // PlayerPos 11
            new DrillWave(playerPositions[11], enemyPrefabs[0], new SpawnBatch(spawnPoints, 18, 18)), // PlayerPos 12
            new DrillWave(playerPositions[12], enemyPrefabs[0], new SpawnBatch(spawnPoints, 19, 19)), // PlayerPos 13
            new DrillWave(playerPositions[13], enemyPrefabs[0], new SpawnBatch(spawnPoints, 20, 21)), // PlayerPos 14
            new DrillWave(playerPositions[14], enemyPrefabs[0], new SpawnBatch(spawnPoints, 1, 1))  // BOSS
        };

        for (int i = 0; i < createdWaves.Length; i++)
        {
            drillWaves.Add(createdWaves[i]);
        }
    }

    // Sets picked level via player prefs
    private void SetPickedLevel()
    {
        if (PlayerPrefs.HasKey("LevelPick"))
        {
            levelPick = PlayerPrefs.GetInt("LevelPick");
        }
        else
        {
            levelPick = 0;
        }
    }

    // Sets starting position for player
    private void SetStartPosition()
    {
        player.transform.position = drillWaves[levelPick].playerPos.transform.position;
        player.transform.rotation = drillWaves[levelPick].playerPos.transform.rotation;
    }

    // Spawn coroutine for miss mode
    private IEnumerator MissSpawn()
    {
        AM.Play("electric1");
        while (drillWaves[levelPick].amountSpawned <= 5000)
        {
            int posPick = Random.Range(0, drillWaves[levelPick].spawnPoints.pointList.Count);
            GM.totalSpawned++;

            // Instantiates effect
            for (int i = 0; i < drillWaves[levelPick].spawnPoints.pointList.Count; i++)
            {
                // Checks if child count is 0 to only spawn the effect once
                if (drillWaves[levelPick].spawnPoints.pointList[i].transform.childCount == 0)
                {
                    Instantiate(flamePrefab, drillWaves[levelPick].spawnPoints.pointList[i].transform);
                }
            }

            // Instantiates picked enemy at picked position using the parents rotation
            Instantiate(drillWaves[levelPick].enemyPrefab, drillWaves[levelPick].spawnPoints.pointList[posPick].transform.position, drillWaves[levelPick].spawnPoints.pointList[posPick].transform.rotation);
            drillWaves[levelPick].amountSpawned++;

            // Breaks out of coroutine if the game is over or spawned enemies exceed 5000
            if(GM.gameIsOver || drillWaves[levelPick].amountSpawned == 5000)
            {
                GM.SetGameOver();
                yield break;
            }

            yield return new WaitForSeconds(1.2f);
        }
        AM.Play("electric2");
        yield break;
    }
}