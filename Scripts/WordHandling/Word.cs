using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Word {

    private WordDisplay display;
    private WordPosition wordPos;

    public string word;
    public int typeIndex;
    public GameObject worldObject;
    
    // Constructor for Word class
    public Word(string _word, WordDisplay _display, GameObject _worldObject)
    {
        word = _word;
        typeIndex = 0;

        display = _display;
        display.SetWord(word);

        worldObject = _worldObject;
    }

    // Gets next letter of targeted word
    public char GetNextLetter()
    {
        return word[typeIndex];
    }

    // Types next letter of targeted word
    public void TypeLetter()
    {
        typeIndex++;
        display.RemoveLetter();
    }

    // Executes whenever a word has been typed in completely
    public bool WordTyped()
    {
        bool wordTyped = (typeIndex >= word.Length);
        if (wordTyped)
        {
            // Triggers Die animation on enemies or destroys projectile
            if (worldObject && !worldObject.CompareTag("Projectile"))
            {
                display.RemoveWord();
                worldObject.GetComponent<EnemyBehaviour>().Die();
            }
            else
            {
                // display.RemoveWord() will be called from objects script
                worldObject.GetComponent<RockMoss>().Delete();
            }
        }
        return wordTyped;
    }

    // Unselects word and resets the latest selected one
    public void ResetWord()
    {
        display.SetWord(word);
        display.ResetColor();
        typeIndex = 0;
    }
}
