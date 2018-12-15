using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DrillWave
{
    public GameObject playerPos;
    public GameObject enemyPrefab;
    public SpawnBatch spawnPoints;

    public int amountSpawned;

    public DrillWave(GameObject _playerPos, GameObject _enemyPrefab, SpawnBatch _spawnPoints)
    {
        playerPos = _playerPos;
        enemyPrefab = _enemyPrefab;
        spawnPoints = _spawnPoints;

        amountSpawned = 0;
    }
}