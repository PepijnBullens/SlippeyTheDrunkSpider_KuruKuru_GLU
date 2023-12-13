using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreManagerScript : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> highScoreTexts = new List<TextMeshProUGUI>();

    //scene manager script
    private SceneManagerScript sceneManagerScript;

    private void Start()
    {
        //reference scene manager script
        sceneManagerScript = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();

        if (highScoreTexts.Count == 0) return;

        for(int i = 0; i < highScoreTexts.Count; i++)
        {
            if(!PlayerPrefs.HasKey("Level" + i))
            {
                SetHighScore(i, 0, true);
            }

            highScoreTexts[i].text = "HighScore: " + GetHighScore(i).ToString("f3") + " Seconds";
        }
    }

    public void ResetHighScores()
    {
        for(int i = 0; i < highScoreTexts.Count; i++)
        {
            SetHighScore(i, 0, true);
            highScoreTexts[i].text = "HighScore: " + GetHighScore(i).ToString("f3") + " Seconds";
        }
    }

    public void SetHighScore(int level, float time, bool overwrite)
    {
        if(overwrite)
        {
            PlayerPrefs.SetFloat("Level" + level, time);
            return;
        }

        float currentHighScore = PlayerPrefs.GetFloat("Level" + level);

        if (currentHighScore == 0)
        {
            PlayerPrefs.SetFloat("Level" + level, time);
            return;
        }

        if(time < currentHighScore)
        {
            PlayerPrefs.SetFloat("Level" + level, time);
        }
    }

    public float GetHighScore(int level)
    {
        return PlayerPrefs.GetFloat("Level" + level);
    }
}
