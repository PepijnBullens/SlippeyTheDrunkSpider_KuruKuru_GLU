using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1;
    }

    public int GetCurrentScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void CreditsMenu()
    {
        SceneManager.LoadScene("CreditsMenu");
    }

    //quit func
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    //start game
    public void ChooseLevel()
    {
        SceneManager.LoadScene("ChooseLevelMenu");
    }

    public void SetLevel(int level)
    {
        SceneManager.LoadScene("level" + level.ToString("D2"));
    }

    public void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
