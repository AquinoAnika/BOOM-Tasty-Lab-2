using UnityEngine;

public class CreditManager : MonoBehaviour
{
    public GameObject Credits;
    public GameObject MainMenuPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape Key Pressed!"); // Check your Console for this!
            if (Credits.activeSelf)
            {
                ReturnToMenu();
            }
        }
    }

    public void ShowCredits()
    {
        Credits.SetActive(true);
        MainMenuPanel.SetActive(false);
    }

    public void ReturnToMenu()
    {
        Credits.SetActive(false);
        MainMenuPanel.SetActive(true);
    }
}
