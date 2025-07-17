using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    [SerializeField] private Slider playerHealthSlider;
    [SerializeField] private TMP_Text playerHealthText;
    
    [SerializeField] private Slider playerExperienceSlider;
    [SerializeField] private TMP_Text playerExperienceText;
    
    [SerializeField] private TMP_Text timerText;
    
    public GameObject gameOverPanel;
    public GameObject pausePanel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    public void UpdateExperienceSlider()
    {
        playerExperienceSlider.maxValue = Player.Instance.playerLevels[
            Player.Instance.currentLevel - 1
        ];
        playerExperienceSlider.value = Player.Instance.experience;
        playerExperienceText.text = $"{playerExperienceSlider.value} / {playerExperienceSlider.maxValue}";
    }
    
    public void UpdateHealthSlider()
    {
        playerHealthSlider.maxValue = Player.Instance.playerMaxHealth;
        playerHealthSlider.value = Player.Instance.playerCurrentHealth;
        playerHealthText.text = $"{playerHealthSlider.value} / {playerHealthSlider.maxValue}";
    }

    public void UpdateTimerText(float time)
    {
        float min = Mathf.FloorToInt(time / 60f);
        float sec = Mathf.FloorToInt(time % 60f);

        timerText.text = $"{min}:{sec:00}";
    }
}