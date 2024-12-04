using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    private bool isPaused = false;


    private void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMenu();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleTimeScale();
        }
    }

    void OnDisable()
    {
        Time.timeScale = 1.0f;
    }

    private void PlayGame()
    {
        SceneManager.LoadScene("MainGame");
        Time.timeScale = 1;
        
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void Quitgame()
    {
        Application.Quit();
    }

    private void ToggleTimeScale()
    {
        if (isPaused)
        {
            Time.timeScale = 1; 
        }
        else
        {
            Time.timeScale = 0; 
        }

        isPaused = !isPaused; 
    }
}
