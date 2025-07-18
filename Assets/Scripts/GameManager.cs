using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool gameActive;
    public float gameTime;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    void Start()
    {
        gameActive = true;
    }

    void Update()
    {
        bool isInMainMenuScene = SceneManager.GetActiveScene().name == "MainMenu";
        
        // ignore game updates if
        // game is Not Active or in Main Menu Scene
        if (!gameActive || isInMainMenuScene) return;
        
        // update game timer
        gameTime += Time.deltaTime;
        UIController.Instance.UpdateTimerText(gameTime);
        
        // toggle game pause state on escape key pressed
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            TogglePause();
    }

    public void GameOver()
    {
        gameActive = false;
        StartCoroutine(ShowGameOverScreen());
    }

    IEnumerator ShowGameOverScreen()
    {
        yield return new WaitForSeconds(1.5f);
        UIController.Instance.gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Scenes/Game");
    }

    private void TogglePause()
    {
        // ignore if game over screen is already shown
        if (UIController.Instance.gameOverPanel.activeSelf) return;
        
        bool newPausedState = !UIController.Instance.pausePanel.activeSelf;
        UIController.Instance.pausePanel.SetActive(newPausedState);
        Time.timeScale = newPausedState ? 0f : 1f;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}