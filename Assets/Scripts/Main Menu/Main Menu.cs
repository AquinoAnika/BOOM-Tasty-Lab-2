using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartGame()
{
    // Replace "NameOfYourGameScene" with the actual name of your game scene
    SceneManager.LoadScene("Boomtasty");
}

    // Update is called once per frame
    public void QuitGame()
    {
        Application.Quit();
    }
}
