using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    
    public void LaunchMainMenu()
    {
        
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Additive);

        
        GetComponent<Animator>().enabled = false;
    }
}