using UnityEngine;

public class GameFlow : MonoBehaviour
{
    public GameObject introObject;
    public GameObject mainMenuObject;
    public float delayTime = 5.0f;

    void Start()
    {
        // If the intro has already played, skip it!
        if (GlobalState.hasIntroPlayed)
        {
            introObject.SetActive(false);
            mainMenuObject.SetActive(true);
        }
        else
        {
            // Otherwise, play the intro and mark it as played
            introObject.SetActive(true);
            mainMenuObject.SetActive(false);
            Invoke("ShowMenu", delayTime);
            GlobalState.hasIntroPlayed = true;
        }
    }

    void ShowMenu()
    {
        introObject.SetActive(false);
        mainMenuObject.SetActive(true);
    }
}