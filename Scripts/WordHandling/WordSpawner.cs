using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawner : MonoBehaviour {

    public GameObject wordPrefab;
    public Transform wordCanvas;
    public WordManager wordManager;

    // Spawns word on canvas
    public WordDisplay SpawnWord(GameObject worldObject)
    {
        Vector3 spawnPos = Camera.main.WorldToScreenPoint(worldObject.transform.position);
        WordPosition wordPosition = worldObject.GetComponent<WordPosition>();

        GameObject textObj = Instantiate(wordPrefab, spawnPos, Quaternion.identity, wordCanvas);
        WordDisplay wordDisplay = textObj.GetComponentInChildren<WordDisplay>();
        wordPosition.SetTextObject(textObj);
        wordDisplay.SetWorldObject(worldObject);

        return wordDisplay;
    }
}
