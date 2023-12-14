using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    //start
    private void Start()
    {
        Time.timeScale = 1;
    }

    //get current scene func
    public int GetCurrentScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    //go to main scene func
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //go to credits scene func
    public void CreditsMenu()
    {
        SceneManager.LoadScene("CreditsMenu");
    }

    //quit game func
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    //go to chooselevel scene func
    public void ChooseLevel()
    {
        SceneManager.LoadScene("ChooseLevelMenu");
    }

    //go to given level scene func
    public void SetLevel(int level)
    {
        SceneManager.LoadScene("level" + level.ToString("D2"));
    }

    //go to next level scene func
    public void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
