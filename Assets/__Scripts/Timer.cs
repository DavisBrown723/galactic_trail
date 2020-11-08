using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
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
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }// end Update()
}// end class Timer
