using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    [Header("Script Reference's")]

    [SerializeField]
    private PlayerScript playerScript;
    [SerializeField]
    private PlayerCollisionScript playerCollisionScript;


    //menu manager script
    private MenuManagerScript menuManagerScript;
    //tmer manager script
    private TimerManagerScript timerManagerScript;
    //highscore manager script
    private HighScoreManagerScript highScoreManagerScript;
    //scene manager script
    private SceneManagerScript sceneManagerScript;


    //pick up bottle script reference
    private PickUpBottleScript pickUpBottleScript;

    //mini map reference
    private GameObject miniMap;

    //keep track of if game is playing
    private bool isGamePlaying = true;

    private int currentMenu;

    //start
    private void Start()
    {
        //objects only made once and easy to find in scene are referenced by "GameObject.Find()"

        //reference menu manager script
        menuManagerScript = GameObject.Find("MenuManager").GetComponent<MenuManagerScript>();
        //reference timer manager script
        timerManagerScript = GameObject.Find("TimerManager").GetComponent<TimerManagerScript>();
        //reference checkpoint manager script
        highScoreManagerScript = GameObject.Find("HighScoreManager").GetComponent<HighScoreManagerScript>();
        //reference scene manager script
        sceneManagerScript = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();

        //find pick up bottle (only if pick up bottle is in scene)
        if (GameObject.Find("PickUpBottle") != null)
        {
            pickUpBottleScript = GameObject.Find("PickUpBottle").GetComponent<PickUpBottleScript>();
        }

        //find mini map
        miniMap = GameObject.Find("MiniMapUI");
    }

    //update
    private void Update()
    {
        //check if escape was pressed
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            //call pause game func
            PauseGame();
        }
    }

    //pause game func
    public void PauseGame()
    {
        if (currentMenu != 0 && currentMenu != 3) return;

        if (currentMenu == 3) currentMenu = 0;
        else currentMenu = 3;

        //if game is playing, pause
        //if game is paused, play
        isGamePlaying = !isGamePlaying;

        //set time scale according to the isGamePlaying variable
        if(isGamePlaying) Time.timeScale = 1;
        else Time.timeScale = 0;

        //set state of minimap, timer, pausescreen to according to isGamePlaying variable
        miniMap.SetActive(isGamePlaying);
        timerManagerScript.SetTimerState(isGamePlaying);
        menuManagerScript.PauseScreen(!isGamePlaying);
    }

    //restart func
    public void Restart()
    {
        //reload this scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //lose func
    public void Lose()
    {
        if (currentMenu != 0 && currentMenu != 1) return;

        if (currentMenu == 1) currentMenu = 0;
        else currentMenu = 1;

        //make player unable to move
        playerScript.SetCanMove(false);

        //enable game over screen
        menuManagerScript.StartCoroutine(menuManagerScript.LoseScreen(true, true));
    }

    //win func
    public void Win()
    {
        if (currentMenu != 0 && currentMenu != 2) return;

        if (currentMenu == 2) currentMenu = 0;
        else currentMenu = 2;

        //stop timer
        timerManagerScript.SetTimerState(false);

        highScoreManagerScript.SetHighScore(sceneManagerScript.GetCurrentScene() - 3, timerManagerScript.GetTime(), false);

        //stop movement
        playerScript.StopMovement();

        //activate win screen
        menuManagerScript.WinScreen(true, true);

        //make player unable to move
        playerScript.SetCanMove(false);

        //enable game over screen
        menuManagerScript.StartCoroutine(menuManagerScript.WinScreen(true, true));
    }
}