using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public KeyCode resetTimeKey = KeyCode.Space; 
    public float transitionDuration = 2.0f; 

    private bool isResetting = false; 
    private float elapsedTime = 0.0f; 


    private void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {

        if (Input.GetKeyDown(resetTimeKey) && !isResetting)
        {
            isResetting = true;
            elapsedTime = 0.0f;
        }

        if (isResetting)
        {
            elapsedTime += Time.unscaledDeltaTime; 
            float t = elapsedTime / transitionDuration;

            Time.timeScale = Mathf.Lerp(0, 1, t);


            if (t >= 1.0f)
            {
                Time.timeScale = 1.0f;
                isResetting = false;
            }
        }

        // Back to menu with Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMenu(); // Call BackToMenu when Escape is pressed
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


    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void Quitgame()
    {
        Application.Quit();
    }
}
