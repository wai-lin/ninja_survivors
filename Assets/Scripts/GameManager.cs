using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    public void GameOver()
    {
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

    public void TogglePause()
    {
        // ignore if game over screen is already shown
        if (UIController.Instance.gameOverPanel.activeSelf) return;
        
        bool newPausedState = !UIController.Instance.pausePanel.activeSelf;
        UIController.Instance.pausePanel.SetActive(newPausedState);
        Time.timeScale = newPausedState ? 0f : 1f;
    }
}