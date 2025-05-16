using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameEndingManager : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 2f;
    public float delayBeforeFade = 1f;
    public string endingSceneName = "MainMenu"; // or "Credits"

    public GameObject victoryText; // Optional UI text like "Victory!" or "Thanks for Playing"

    public void TriggerGameEnd()
    {
        StartCoroutine(EndGameSequence());
    }

    IEnumerator EndGameSequence()
    {
        // Optional: Show text
        if (victoryText)
            victoryText.SetActive(true);

        yield return new WaitForSeconds(delayBeforeFade);

        // Start fading
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }

        // Load next scene (e.g., Main Menu or Credits)
        SceneManager.LoadScene(endingSceneName);
    }
}
