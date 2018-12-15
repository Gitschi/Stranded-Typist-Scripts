using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    private GameManager GM;

    private void Awake()
    {
        GM = FindObjectOfType<GameManager>();
    }

    // Unpause and resume game
    public void ResumeGame()
    {
        GM.Pause();
    }

    // Return to main menu
    public void BackToMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

    // Restart same scene with same settings
    public void RestartGame()
    {
        switch (PlayerPrefs.GetString("mode"))
        {
            case "Arcade Mode":
                SceneManager.LoadScene(1);
                break;

            case "Miss Mode":
                SceneManager.LoadScene(2);
                break;

            case "Speed Mode":
                SceneManager.LoadScene(2);
                break;

            default:
                SceneManager.LoadScene(0);
                break;
        }
    }
}
