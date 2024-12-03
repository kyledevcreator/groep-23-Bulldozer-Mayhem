using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        Time.timeScale = 1;
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("MainG");
        Time.timeScale = 1;
    }

    //public void Options()
    //{
    //    SceneManager.LoadScene(3);
    //}

    public void Quitgame()
    {
        Application.Quit();
    }
}
