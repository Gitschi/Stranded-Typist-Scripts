using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public int enemyAmount;
    public float spawnDelay;
    public int waveType;
    public GameObject[] enemyPrefabs;
    public SpawnBatch spawnPoints;

    public int amountSpawned;

    public Wave(int _enemyAmount, float _spawnDelay, int _waveType, GameObject[] _enemyPrefabs, SpawnBatch _spawnPoints)
    {
        enemyAmount = _enemyAmount;
        spawnDelay = _spawnDelay;
        waveType = _waveType;
        enemyPrefabs = _enemyPrefabs;
        spawnPoints = _spawnPoints;

        amountSpawned = 0;
    }
}
