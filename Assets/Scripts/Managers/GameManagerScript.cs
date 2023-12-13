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
    //checkpoint manager script
    private CheckpointManagerScript checkpointManagerScript;
    //highscore manager script
    private HighScoreManagerScript highScoreManagerScript;
    //scene manager script
    private SceneManagerScript sceneManagerScript;

    private PickUpBottleScript pickUpBottleScript;


    private GameObject miniMap;

    private bool isGamePlaying = true;

    private void Start()
    {
        //objects only made once and easy to find in scene are referenced by "GameObject.Find()"

        //reference menu manager script
        menuManagerScript = GameObject.Find("MenuManager").GetComponent<MenuManagerScript>();
        //reference timer manager script
        timerManagerScript = GameObject.Find("TimerManager").GetComponent<TimerManagerScript>();
        //reference checkpoint manager script
        checkpointManagerScript = GameObject.Find("CheckpointManager").GetComponent<CheckpointManagerScript>();
        //reference checkpoint manager script
        highScoreManagerScript = GameObject.Find("HighScoreManager").GetComponent<HighScoreManagerScript>();
        //reference scene manager script
        sceneManagerScript = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();

        if (GameObject.Find("PickUpBottle") != null)
        {
            pickUpBottleScript = GameObject.Find("PickUpBottle").GetComponent<PickUpBottleScript>();
        }

        miniMap = GameObject.Find("MiniMapUI");
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        isGamePlaying = !isGamePlaying;

        if(isGamePlaying) Time.timeScale = 1;
        else Time.timeScale = 0;

        miniMap.SetActive(isGamePlaying);
        timerManagerScript.SetTimerState(isGamePlaying);
        menuManagerScript.PauseScreen(!isGamePlaying);
    }

    //restart func
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //lose func
    public void Lose()
    {
        //make player unable to move
        playerScript.SetCanMove(false);

        //enable game over screen
        menuManagerScript.StartCoroutine(menuManagerScript.LoseScreen(true, true));
    }

    //win func
    public void Win()
    {
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