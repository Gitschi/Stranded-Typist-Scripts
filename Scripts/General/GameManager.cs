using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private Audiomanager AM;
    private DrillModeManager drillModeManager;
    private InterfaceController interfaceController;
    private PlayerBehaviour playerBehaviour;
    private ScreenShake screenShake;
    private HealthHandler healthHandler;
    private int pointsPerKill;
    private int minusPointsPerMiss;
    private bool gameIsPaused;

    public GameMode gameMode;
    public bool gameIsOver;
    public int totalSpawned;
    public int totalDefeated;
    public int typingErrors;
    public int currentScore;
    public bool hasActiveProjectile;

    private void Awake()
    {
        AM = FindObjectOfType<Audiomanager>();
        interfaceController = FindObjectOfType<InterfaceController>();
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();
        screenShake = FindObjectOfType<ScreenShake>();
        healthHandler = FindObjectOfType<HealthHandler>();
        SetGameMode();

        if(gameMode != GameMode.Arcade)
        {
            drillModeManager = FindObjectOfType<DrillModeManager>();
        }
    }

    private void Start()
    {
        pointsPerKill = 100;
        minusPointsPerMiss = 50;
        gameIsPaused = false;
        gameIsOver = false;
        totalSpawned = 0;
        totalDefeated = 0;
        typingErrors = 0;
        currentScore = 0;
        hasActiveProjectile = false;

        AM.Play("fear3");
        AM.Play("ambience_wind");
    }

    // Sets gameMode via player prefs
    private void SetGameMode()
    {
        int gameModePick = 0;
        if (PlayerPrefs.HasKey("GameMode"))
        {
            gameModePick = PlayerPrefs.GetInt("GameMode");
        }

        switch (gameModePick)
        {
            case 0:
                gameMode = GameMode.Arcade;
                break;

            case 1:
                gameMode = GameMode.Speed;
                break;

            case 2:
                gameMode = GameMode.Miss;
                break;
        }
    }

    // Increments the defeated enemies
    public void IncrementDefeated()
    {
        totalDefeated++;
        interfaceController.SetDefeated();

        // Lets the player behaviour know if a wave is defeated and he should move on
        if (playerBehaviour)
        {
            SetOnRails();
        }
    }

    // Sets player back on rails
    public void SetOnRails()
    {
        if (!hasActiveProjectile && totalSpawned <= totalDefeated)
        {
            playerBehaviour.gameState = GameStates.OnRail;
        }
    }

    // All logic to be executed on game over
    public void SetGameOver()
    {
        gameIsOver = true;
        SetScoreData();
        AM.Play("magic_bell");
        SceneManager.LoadScene(3);
    }

    // Sets data for score screen on playerPrefs
    private void SetScoreData()
    {
        PlayerPrefs.SetInt("score", currentScore);
        PlayerPrefs.SetInt("time", (int)interfaceController.timer);

        if(gameMode == GameMode.Arcade)
        {
            PlayerPrefs.SetString("mode", "Arcade Mode");
        }
        else if(gameMode == GameMode.Miss)
        {
            PlayerPrefs.SetString("mode", "Miss Mode");
        }
        else if(gameMode == GameMode.Speed)
        {
            PlayerPrefs.SetString("mode", "Speed Mode");
        }
    }

    // Resets spawn in speed mode
    public void SpeedModeSpawnReset()
    {
        if(gameMode == GameMode.Speed)
        {
            drillModeManager.speedSpawnPossible = true;
        }
    }

    // Increment score when defeating an enemy
    public void IncrementScore()
    {
        currentScore += pointsPerKill;
        interfaceController.SetScore();
    }

    // Reduce score when damaged or missing
    public void ReduceScore()
    {
        if(currentScore >= minusPointsPerMiss)
        {
            currentScore -= minusPointsPerMiss;
            interfaceController.SetScore();
            interfaceController.FlashScoreReduction();
        }
    }

    // Flashes screen and lowers score when being damaged
    public void DamagePlayer()
    {
        interfaceController.DamageFlash();
        healthHandler.ReduceHealth();
        ReduceScore();
        ShakeScreen();
    }

    // Pauses game and shows overlay
    public void Pause()
    {
        if (!gameIsPaused)
        {
            gameIsPaused = true;
            Time.timeScale = 0.0f;
            interfaceController.ActivatePauseOverlay();
        }
        else
        {
            gameIsPaused = false;
            Time.timeScale = 1.0f;
            interfaceController.DeactivatePauseOverlay();
        }
    }

    // Starts coroutine for screen shake
    public void ShakeScreen()
    {
        StartCoroutine(screenShake.Shake(0.2f, 0.01f));
    }
}

// Enum of game modes
public enum GameMode
{
    Arcade,
    Speed,
    Miss
}