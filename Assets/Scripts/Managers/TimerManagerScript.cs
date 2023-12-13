using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerManagerScript : MonoBehaviour
{
    [Header("Timer Texts")]

    [SerializeField]
    private GameObject gamePlayTimer;
    [SerializeField]
    private TextMeshProUGUI winSreenTimerText;

    //timer
    private float timer = 0;
    //timer state
    private bool timerState = false;


    //reset timer func
    public void ResetTimer()
    {
        timerState = false;
        timer = 0;
    }

    //set timer state
    public void SetTimerState(bool state)
    {
        timerState = state;
    }

    //get timer state
    public bool GetTimerState()
    {
        return timerState;
    }

    public float GetTime()
    {
        return timer;
    }

    //update
    private void Update()
    {
        //if timer should be on
        if (timerState)
        {
            //activate timer
            gamePlayTimer.SetActive(true);
            //start timer
            timer += Time.deltaTime;
        }
        else gamePlayTimer.SetActive(false); //otherwise deactivate timer

        //update gameplay timer
        gamePlayTimer.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = timer.ToString("f3") + " Seconds";
        //update win screen timer
        winSreenTimerText.text = timer.ToString("f3") + " Seconds";
    }
}
