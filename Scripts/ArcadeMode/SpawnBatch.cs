using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnBatch
{
    public List<GameObject> pointList = new List<GameObject>();

    public SpawnBatch(List<GameObject> allSpawns, int startValue, int endValue)
    {
        for(int i = startValue; i <= endValue; i++)
        {
            pointList.Add(allSpawns[i]);
        }
    }
}