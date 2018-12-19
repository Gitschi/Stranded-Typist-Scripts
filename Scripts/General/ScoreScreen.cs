using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScreen : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI modeName;
    [SerializeField] private TextMeshProUGUI wordLength;
    [SerializeField] private TextMeshProUGUI stage;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI time;

    [SerializeField] private GameObject stageDisplay;

    void Start () {
        SetModeName();
        SetWordLength();
        SetStage();
        SetScore();
        SetTime();
	}
	
    // Sets mode name
    private void SetModeName()
    {
        modeName.text = PlayerPrefs.GetString("mode");
    }

    // Sets word length
    private void SetWordLength()
    {
        string displayString = "Word Length: ";

        switch (PlayerPrefs.GetInt("wordLength"))
        {
            case 0:
                displayString += "Very Short";
                break;

            case 1:
                displayString += "Short";
                break;

            case 2:
                displayString += "Normal";
                break;

            case 3:
                displayString += "Long";
                break;

            case 4:
                displayString += "Very Long";
                break;
        }

        wordLength.text = displayString;
    }

    // Sets stage if not arcade mode
    private void SetStage()
    {
        if(PlayerPrefs.GetString("mode") != "Arcade Mode")
        {
            int levelNum = PlayerPrefs.GetInt("LevelPick");
            stage.text = "Stage " + levelNum.ToString();
            stageDisplay.SetActive(true);
        }
        else
        {
            stageDisplay.SetActive(false);
        }
    }

    // Sets score
    private void SetScore()
    {
        score.text = "Your Score: " + PlayerPrefs.GetInt("score").ToString();
    }

    // Sets time
    private void SetTime()
    {
        time.text = "Your Time: " + PlayerPrefs.GetInt("time").ToString() + " seconds";
    }
}
