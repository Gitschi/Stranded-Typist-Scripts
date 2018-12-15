using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour {

    private bool hasActiveWord;
    private Word activeWord;
    private GameManager GM;
    private Audiomanager AM;
    private InterfaceController interfaceController;

    public List<Word> words;
    public List<char> startChars;
    public WordSpawner wordSpawner;

    private void Awake()
    {
        GM = FindObjectOfType<GameManager>();
        AM = FindObjectOfType<Audiomanager>();
        interfaceController = FindObjectOfType<InterfaceController>();
    }

    // Generates words and adds them to list
    public void AddWord(GameObject worldObject)
    {
        Word word = new Word(WordGenerator.GetRandomWord(words, startChars), wordSpawner.SpawnWord(worldObject), worldObject);

        startChars.Add(word.word[0]);
        words.Add(word);
    }

    // Handles typing of every single letter
    public void TypeLetter(char letter)
    {
        if (hasActiveWord)
        {
            if(activeWord.GetNextLetter() == letter)
            {
                activeWord.TypeLetter();
                AM.Play("thud_bright");
            }
            else
            {
                GM.ShakeScreen();
                AM.Play("alarm");
                interfaceController.ShowMissText();
                GM.ReduceScore();

                // Increments typing errors on gameManager
                GM.typingErrors++;

                if (GM.gameMode == GameMode.Miss)
                {
                    GM.Invoke("SetGameOver", 0.5f);
                }
            }
        }
        else
        {
            foreach(Word word in words)
            {
                if(word.GetNextLetter() == letter)
                {
                    AM.Play("thud_bright");
                    activeWord = word;
                    hasActiveWord = true;
                    word.TypeLetter();
                    break;
                }
            }
        }

        // Resets when you typed the last letter
        if(hasActiveWord && activeWord.WordTyped())
        {
            hasActiveWord = false;
            startChars.Remove(activeWord.word[0]);
            words.Remove(activeWord);

            AM.Play("generic_spell7");
            GM.IncrementScore();
        }
    }

    // Resets when hitting Tab
    public void TargetReset()
    {
        if (hasActiveWord)
        {
            hasActiveWord = false;
            activeWord.ResetWord();
            activeWord = null;
        }
    }

    // Find word in List and delete
    public void DeleteProjectileWord(WordDisplay wordDisplay)
    {
        Word listWord = words.Find(x => x.word == wordDisplay.text.text);
        words.Remove(listWord);
    }

    // Find initial in list and delete. Also resets target
    public void DeleteProjectileChar(WordDisplay wordDisplay)
    {
        TargetReset();

        char listChar = startChars.Find(x => x == wordDisplay.text.text[0]);
        startChars.Remove(listChar);
    }
}
