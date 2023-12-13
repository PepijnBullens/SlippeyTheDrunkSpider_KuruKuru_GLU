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
        gameOverScreen.SetActive(false);
        winScreen.SetActive(false);
    }

    public void PauseScreen(bool state)
    {
        pauseScreen.SetActive(state);
    }

    //IEnumerator that enables game over screen
    public IEnumerator LoseScreen(bool state, bool wait)
    {
        //wait
        if(wait) yield return new WaitForSeconds(loseScreenDelay);

        //enable or disable
        gameOverScreen.SetActive(state);
    }

    public IEnumerator WinScreen(bool state, bool wait)
    {
        //wait
        if(wait) yield return new WaitForSeconds(winScreenDelay);

        //enable or disable
        winScreen.SetActive(state);
    }
}
