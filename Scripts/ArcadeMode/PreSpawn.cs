using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PreSpawn
{
    public List<GameObject> spawnPoints = new List<GameObject>();
    public GameObject spawnBatch;
    public GameObject enemyPrefab;

    public PreSpawn(GameObject _spawnBatch, GameObject[] _spawnPoints, int pointStart, int pointEnd, GameObject _enemyPrefab)
    {
        spawnBatch = _spawnBatch;
        
        for(int i = pointStart; i <= pointEnd; i++)
        {
            spawnPoints.Add(_spawnPoints[i]);
        }

        enemyPrefab = _enemyPrefab;
    }
}
