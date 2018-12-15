using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHandler : MonoBehaviour {

    private GameManager GM;
    private float maxHealth;
    private float currentHealth;
    private float regenRate;
    private float incomingDamage;

    public Slider healthBar;

    private void Awake()
    {
        GM = FindObjectOfType<GameManager>();
    }

    // Use this for initialization
    void Start () {
        maxHealth = 100.0f;
        regenRate = 2.0f;
        incomingDamage = 25.0f;

        currentHealth = maxHealth;
        UpdateHealth();
	}
	
	// Update is called once per frame
	void Update () {
        if(currentHealth < 100.0f)
        {
            if(currentHealth > 0.0f)
            {
                currentHealth += Time.deltaTime * regenRate;
                UpdateHealth();
            }
            else
            {
                GM.SetGameOver();
            }
        }
		else if(currentHealth > 100.0f)
        {
            currentHealth = 100.0f;
        }
	}

    // Calculates health for slider
    private void UpdateHealth()
    {
        healthBar.value = currentHealth / maxHealth;
    }

    // Reduces player health
    public void ReduceHealth()
    {
        currentHealth -= incomingDamage;
        UpdateHealth();
    }
}
