using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }
    
    public void NewGame()
    {
        SceneManager.LoadScene("Scenes/Game");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
