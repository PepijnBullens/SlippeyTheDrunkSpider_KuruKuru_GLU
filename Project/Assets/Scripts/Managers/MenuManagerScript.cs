using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagerScript : MonoBehaviour
{
    [Header("Screen Delays")]

    [SerializeField]
    private float winScreenDelay;
    [SerializeField]
    private float loseScreenDelay;


    [Header("UI Screens")]

    [SerializeField]
    private GameObject gameOverScreen;
    [SerializeField]
    private GameObject winScreen;
    [SerializeField]
    private GameObject pauseScreen;



    //start
    private void Start()
    {
        //set lose and win screen inactive
        gameOverScreen.SetActive(false);
        winScreen.SetActive(false);
    }

    //set losescreen active or inactive
    public IEnumerator LoseScreen(bool state, bool wait)
    {
        //wait
        if (wait) yield return new WaitForSeconds(loseScreenDelay);

        //enable or disable
        gameOverScreen.SetActive(state);
    }

    //set winscreen active or inactive
    public IEnumerator WinScreen(bool state, bool wait)
    {
        //wait
        if (wait) yield return new WaitForSeconds(winScreenDelay);

        //enable or disable
        winScreen.SetActive(state);
    }

    //set pausescreen active or inactive
    public void PauseScreen(bool state)
    {
        pauseScreen.SetActive(state);
    }
}
