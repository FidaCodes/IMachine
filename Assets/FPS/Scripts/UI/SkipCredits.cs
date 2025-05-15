using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipCredits : MonoBehaviour
{
    public string mainMenuSceneName = "IntroMenu"; // Replace with your actual main menu scene name

    public void SkipToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
