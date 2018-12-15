using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject flamePrefab;
    [SerializeField] private Transform enemyHolder;
    private GameManager GM;
    private Audiomanager AM;

    public List<Wave> waves;
    public List<GameObject> spawnPoints = new List<GameObject>();
    public GameObject[] enemyPrefabs;

    private void Awake()
    {
        GM = FindObjectOfType<GameManager>();
        AM = FindObjectOfType<Audiomanager>();
        spawnPoints.AddRange(GameObject.FindGameObjectsWithTag("SpawnPoint"));
        SortSpawnPoints();
    }

    private void Start()
    {
        AddWaves();
    }

    // Adds waves to list
    public void AddWaves()
    {
        Wave[] createdWaves =
        {
            new Wave(5, 1.5f, 0, enemyPrefabs, new SpawnBatch(spawnPoints, 0, 1)),    // Waypoint 3
            new Wave(3, 1.5f, 1, enemyPrefabs, new SpawnBatch(spawnPoints, 2, 2)),    // Waypoint 4
            new Wave(7, 1.2f, 0, enemyPrefabs, new SpawnBatch(spawnPoints, 3, 4)),    // Waypoint 5
            new Wave(5, 1.3f, 1, enemyPrefabs, new SpawnBatch(spawnPoints, 5, 6)),    // Waypoint 6
            new Wave(5, 1.3f, 0, enemyPrefabs, new SpawnBatch(spawnPoints, 7, 7)),    // Waypoint 7
            new Wave(8, 1.5f, 0, enemyPrefabs, new SpawnBatch(spawnPoints, 8, 9)),    // Waypoint 10
            new Wave(10, 1.5f, 1, enemyPrefabs, new SpawnBatch(spawnPoints, 10, 12)), // Waypoint 12
            new Wave(5, 1.5f, 0, enemyPrefabs, new SpawnBatch(spawnPoints, 13, 13)),  // Waypoint 14
            new Wave(6, 1.2f, 1, enemyPrefabs, new SpawnBatch(spawnPoints, 14, 15)),  // Waypoint 16
            new Wave(8, 2f, 1, enemyPrefabs, new SpawnBatch(spawnPoints, 16, 16)),    // Waypoint 17
            new Wave(4, 1.5f, 0, enemyPrefabs, new SpawnBatch(spawnPoints, 17, 17)),  // Waypoint 18
            new Wave(7, 1.5f, 0, enemyPrefabs, new SpawnBatch(spawnPoints, 18, 18)),  // Waypoint 20
            new Wave(5, 1.2f, 0, enemyPrefabs, new SpawnBatch(spawnPoints, 19, 19)),  // Waypoint 22
            new Wave(8, 1.5f, 1, enemyPrefabs, new SpawnBatch(spawnPoints, 20, 21))   // Waypoint 23
        };

        for(int i = 0; i < createdWaves.Length; i++)
        {
            waves.Add(createdWaves[i]);
        }
    }

    // Sorts spawnpoints by name
    public void SortSpawnPoints()
    {
        if (spawnPoints.Count > 0)
        {
            spawnPoints.Sort(delegate (GameObject a, GameObject b)
            {
                return (a.name).CompareTo(b.name);
            });
        }
    }

    // Starts spawning coroutine
    public void StartSpawn()
    {
        StartCoroutine("Spawn", waves[0]);
        AM.Play("electric1");
    }

    // Keeps on spawning as long as the spawned amount is lower than the requested amount
    private IEnumerator Spawn(Wave wave)
    {
        GM.totalSpawned += wave.enemyAmount;

        // Handles effect
        List<GameObject> effectList = new List<GameObject>();
        for(int i = 0; i < wave.spawnPoints.pointList.Count; i++)
        {
            GameObject go = Instantiate(flamePrefab, wave.spawnPoints.pointList[i].transform);
            effectList.Add(go);
        }

        while (wave.amountSpawned < wave.enemyAmount)
        {
            int posPick = Random.Range(0, wave.spawnPoints.pointList.Count);
            int enemyPick = wave.waveType;

            // Instantiates picked enemy at picked position using the parents rotation
            GameObject go = Instantiate(wave.enemyPrefabs[enemyPick], wave.spawnPoints.pointList[posPick].transform.position, wave.spawnPoints.pointList[posPick].transform.rotation);
            go.transform.parent = enemyHolder;

            wave.amountSpawned++;
            yield return new WaitForSeconds(wave.spawnDelay);
        }

        // Destroys effects
        for(int i = 0; i < effectList.Count; i++)
        {
            Destroy(effectList[i]);
        }
        AM.Play("electric2");

        // Removes wave from list and breaks
        waves.Remove(waves[0]);
        yield break;
    }
}
