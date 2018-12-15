using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGenerator : MonoBehaviour {

    private string csvFileName;
    private string[] csvContent;
    private static string[] wordList;

    private void Start()
    {
        LoadFileData();
    }

    // Gets a random word from the dictionary and returns it
    public static string GetRandomWord(List<Word> words, List<char> startChars)
    {
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, wordList.Length);
        }
        while (!UniqueInitialCheck(wordList[randomIndex], startChars));

        return wordList[randomIndex];
    }

    // Checks if the initial char of the word is unique, to prevent two words with the same initial from existing
    public static bool UniqueInitialCheck(string pickedWord, List<char> startChars)
    {
        bool isUnique = true;
        
        for(int i = 0; i < startChars.Count; i++)
        {
            if (pickedWord[0] == startChars[i])
            {
                isUnique = false;
                break;
            }
        }

        return isUnique;
    }

    // Loads data from file
    private void LoadFileData()
    {
        if (PlayerPrefs.HasKey("wordLength"))
        {
            switch (PlayerPrefs.GetInt("wordLength")){
                case 0: // very short
                    csvFileName = "wordListVeryShort";
                    break;

                case 1: // short
                    csvFileName = "wordListShort";
                    break;

                case 2: // normal
                    csvFileName = "wordListNormal";
                    break;

                case 3: // long
                    csvFileName = "wordListLong";
                    break;

                case 4: // very long
                    csvFileName = "wordListVeryLong";
                    break;
            }
        }
        else
        {
            csvFileName = "wordListNormal";
        }

        TextAsset txtAssets = (TextAsset)Resources.Load(csvFileName);
        csvContent = txtAssets.text.Split(new char[] {','});
        wordList = csvContent;
    }
}
