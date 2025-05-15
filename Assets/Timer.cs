using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    private bool gameOverTriggered = false;

    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime <= 0 && !gameOverTriggered)
            {
                remainingTime = 0;
                timerText.color = Color.red;
                TriggerGameOver();
            }
        }

        //remainingTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        void TriggerGameOver()
        {
            gameOverTriggered = true;
            SceneManager.LoadScene("LoseScene");
        }
    }
}
