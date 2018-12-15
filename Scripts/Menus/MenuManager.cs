using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class MenuManager : MonoBehaviour {

    [SerializeField] private GameObject[] menus;
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private TextMeshProUGUI explanationText;
    [SerializeField] private TMP_Dropdown wordLengthDropDown;

    private Audiomanager AM;
    private string[] explanationStrings = {
        "Defeat 30 enemies as fast as possible!",
        "Defeat as many enemies as possible without mistyping!"
    };

    private void Awake()
    {
        AM = FindObjectOfType<Audiomanager>();
    }

    private void Start()
    {
        menus[0].SetActive(true);
        menus[1].SetActive(false);
        loadingPanel.SetActive(false);
        SetStartingWordLength();

        AM.Play("water");
    }

    // Starts arcade mode
    public void StartArcadeMode()
    {
        ButtonSound();
        AM.Stop("water");
        LoadingOverlay();
        SetWordLength();
        PlayerPrefs.SetInt("GameMode", 0);
        SceneManager.LoadScene(1);
    }

    // Sets speed mode in player prefs and opens stagepick menu
    public void StartSpeedMode()
    {
        explanationText.text = explanationStrings[0];
        ButtonSound();
        SetWordLength();
        PlayerPrefs.SetInt("GameMode", 1);
        ToStagePick();
    }

    // Sets miss mode in player prefs and opens stagepick menu
    public void StartMissMode()
    {
        explanationText.text = explanationStrings[1];
        ButtonSound();
        SetWordLength();
        PlayerPrefs.SetInt("GameMode", 2);
        ToStagePick();
    }

    // Sets the stage in player prefs and loads scene
    public void SetDrillStage()
    {
        ButtonSound();
        AM.Stop("water");
        LoadingOverlay();
        string stagePick = EventSystem.current.currentSelectedGameObject.name;
        PlayerPrefs.SetInt("LevelPick", int.Parse(stagePick));
        SceneManager.LoadScene(2);
    }

    // Closes main menu and moves to stage pick
    public void ToStagePick()
    {
        ButtonSound();
        menus[0].SetActive(false);
        menus[1].SetActive(true);
    }

    // Closes stage menu and moves to main menu
    public void StagePickToMainMenu()
    {
        ButtonSound();
        menus[0].SetActive(true);
        menus[1].SetActive(false);
    }

    // Activate loading panel
    public void LoadingOverlay()
    {
        ButtonSound();
        loadingPanel.SetActive(true);
    }

    // Quit game
    public void ExitGame()
    {
        ButtonSound();
        Application.Quit();
    }

    // Play sound on button press
    private void ButtonSound()
    {
        AM.Play("thud_bright");
    }

    // Sets word length on playerPrefs
    private void SetWordLength()
    {
        PlayerPrefs.SetInt("wordLength", wordLengthDropDown.value);
    }

    // Set starting value for word length
    private void SetStartingWordLength()
    {
        if (PlayerPrefs.HasKey("wordLength"))
        {
            wordLengthDropDown.value = PlayerPrefs.GetInt("wordLength");
        }
        else
        {
            wordLengthDropDown.value = 2;
        }
    }
}
