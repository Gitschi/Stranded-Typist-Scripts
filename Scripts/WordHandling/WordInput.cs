using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordInput : MonoBehaviour {

    private GameManager GM;

    public WordManager wordManager;

    private void Awake()
    {
        GM = FindObjectOfType<GameManager>();
    }

    // Gets player input each frame
    void Update () {
        foreach(char letter in Input.inputString)
        {
            wordManager.TypeLetter(letter);
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            wordManager.TargetReset();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GM.Pause();
        }
    }
}
