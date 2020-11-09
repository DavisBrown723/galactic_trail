using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    // 11/08/20 From:
    // https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/
    public float timeRemaining = 60;
    public bool timerIsRunning = false;
    public Text textTime;
   

    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
    }// end Start()

    void DisplayTime(float timeToDisplay){
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);  
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        textTime.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }// end DisplayTime(float)

    void Update(){
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                
                // Here is where we can load the next level, right now it's 
                // just the game over scene
                SceneManager.LoadScene("__Scene_GameOver");

                // Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }// end Update()
}// end class Timer
