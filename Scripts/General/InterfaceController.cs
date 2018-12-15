using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InterfaceController : MonoBehaviour {

    [SerializeField] private GameObject[] interfaceElements;
    [SerializeField] private TextMeshProUGUI[] textElements;
    [SerializeField] private GameObject missText;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject damageFlashPanel;
    [SerializeField] private GameObject pauseOverlay;

    private GameManager GM;
    private Color originalScoreColor;

    public float timer;

    private void Awake()
    {
        GM = FindObjectOfType<GameManager>();
        originalScoreColor = textElements[2].color;
    }

    void Start () {
        timer = 0f;
        damageFlashPanel.SetActive(false);
        pauseOverlay.SetActive(false);
	}
	
	void Update () {
        timer += Time.deltaTime;
        SetTime();
	}

    // Sets time on GUI
    private void SetTime()
    {
        textElements[0].text = "Time: " + timer.ToString("F0");
    }

    // Sets defeated on GUI
    public void SetDefeated()
    {
        textElements[1].text = "Defeated: " + GM.totalDefeated.ToString();
    }

    // Sets score on GUI
    public void SetScore()
    {
        textElements[2].text = "Score: " + GM.currentScore.ToString();
    }

    // Show miss text when player mistypes a letter
    public void ShowMissText()
    {
        GameObject missDisplay = Instantiate(missText, new Vector3(0, 0, 0), Quaternion.identity);
        missDisplay.transform.SetParent(canvas.transform, false);
        Destroy(missDisplay, 0.5f);
    }

    // Display damage flash panel
    public void DamageFlash()
    {
        damageFlashPanel.SetActive(true);
        Invoke("ClearDamageFlash", 0.2f);
    }

    // Clear damage flash panel
    private void ClearDamageFlash()
    {
        damageFlashPanel.SetActive(false);
    }

    // Flash score on reduction
    public void FlashScoreReduction()
    {
        textElements[2].color = new Color(255, 0, 0, 255);
        Invoke("ResetScoreFlash", 1.0f);
    }

    // Reset score flash
    private void ResetScoreFlash()
    {
        textElements[2].color = originalScoreColor;
    }

    // Activates pause overlay
    public void ActivatePauseOverlay()
    {
        pauseOverlay.SetActive(true);
    }

    // Deactivates pause overlay
    public void DeactivatePauseOverlay()
    {
        pauseOverlay.SetActive(false);
    }
}
