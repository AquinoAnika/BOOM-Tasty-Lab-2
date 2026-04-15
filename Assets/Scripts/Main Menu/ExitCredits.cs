using UnityEngine;

public class CreditManager : MonoBehaviour
{
    public GameObject creditsPanel; // The panel with your names
    public GameObject mainMenuUI;   // The object holding your menu buttons (Play, Credits, Exit)

    void Update()
    {
        // If the credits are visible and you press Escape
        if (creditsPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMenu();
        }
    }

    // Function for your Credits Button to call
    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
        mainMenuUI.SetActive(false); // Hide the menu so only credits show
    }

    // Function to return to the menu
    public void ReturnToMenu()
    {
        creditsPanel.SetActive(false);
        mainMenuUI.SetActive(true); // Bring the menu buttons back
    }
}
