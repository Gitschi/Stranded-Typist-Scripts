using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordDisplay : MonoBehaviour {

    private Color standardColor = new Color(1, 1, 1, 1);
    private Color targetColor = new Color(1, 0, 0, 1);
    private GameObject worldObject;
    private PreSpawnHandler preSpawnHandler;
    private GameManager GM;

    public TextMeshProUGUI text;

    private void Awake()
    {
        GM = FindObjectOfType<GameManager>();
        preSpawnHandler = FindObjectOfType<PreSpawnHandler>();
    }

    // Sets the word on the text element
    public void SetWord(string word)
    {
        text.text = word;
    }

    // Removes letter from text object
    public void RemoveLetter()
    {
        text.text = text.text.Remove(0, 1);
        text.color = targetColor;
    }

    // Destroys word gameobject when typing completed and triggers defeated check
    public void RemoveWord()
    {
        Destroy(transform.parent.gameObject);

        // Handle difference between Prespawned and wave enemies
        if (worldObject.CompareTag("WaveEnemy") || worldObject.CompareTag("DrillEnemy"))
        {
            GM.IncrementDefeated();
        }
        else if (worldObject.CompareTag("PreSpawnEnemy"))
        {
            preSpawnHandler.IncrementDefeated();
        }
        else
        {
            return;
        }
    }

    // Sets the world object (enemy or projectile) for word
    public void SetWorldObject(GameObject _worldObject)
    {
        worldObject = _worldObject;
    }

    // Resets color to standard
    public void ResetColor()
    {
        text.color = standardColor;
    }
}
