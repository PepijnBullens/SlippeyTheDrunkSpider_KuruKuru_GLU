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
		//objects only made once and easy to find in scene are referenced by "GameObject.Find()"

		//reference scene manager script
		sceneManagerScript = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
		
		//when there are no text objects given in the highScoreTexts list, dont continue
		if (highScoreTexts.Count == 0) return;

		//for every text in the highScoreTexts list
		for(int i = 0; i < highScoreTexts.Count; i++)
		{
			//if there is no highscore yet
			if(!PlayerPrefs.HasKey("Level" + i))
			{
				//set highscore to 0
				SetHighScore(i, 0, true);
			}

			//set current index of highScoreTexts to the corresponding highscore
			highScoreTexts[i].text = "HighScore: " + GetHighScore(i).ToString("f3") + " Seconds";
		}
	}

	//reset all highscores func
	public void ResetHighScores()
	{
		//for every highscore text
		for(int i = 0; i < highScoreTexts.Count; i++)
		{
			//set current index of highscore to 0 and apply current index of highScoreTexts content to 0
			SetHighScore(i, 0, true);
			highScoreTexts[i].text = "HighScore: " + GetHighScore(i).ToString("f3") + " Seconds";
		}
	}

	//set highscore func
	//takes 3 parameters, what highscore of what level to set
	//time of the highscore
	//and overwrite, to check if the script should compare the given time to the current highscore or not
	public void SetHighScore(int level, float time, bool overwrite)
	{
		//if overwrite is true
		if(overwrite)
		{
			//set
			PlayerPrefs.SetFloat("Level" + level, time);
			//return
			return;
		}

		//get current highscore
		float currentHighScore = PlayerPrefs.GetFloat("Level" + level);

		//if current highscore is 0 (no highscore yet)
		if (currentHighScore == 0)
		{
			//set
			PlayerPrefs.SetFloat("Level" + level, time);
			//return
			return;
		}

		//compare given time with current highscore
		if(time < currentHighScore)
		{
			//set
			PlayerPrefs.SetFloat("Level" + level, time);
		}
	}

	//get highscore func
	public float GetHighScore(int level)
	{
		//return highscore
		return PlayerPrefs.GetFloat("Level" + level);
	}
}
