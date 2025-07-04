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

    public void PlayGame()
    {
        SceneManager.LoadScene("MainGame");
        Time.timeScale = 1;
        
    }

    public void Controls()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reloads the current scene
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



    public void ShopOption1()
    {
        GameManager.Instance.Button(0);
    }
    public void ShopOption2()
    {
        GameManager.Instance.Button(1);
    }
    public void ShopOption3()
    {
        GameManager.Instance.Button(2);
    }
}
