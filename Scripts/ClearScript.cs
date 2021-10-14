using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearScript : MonoBehaviour
{   public static int score;
    // Start is called before the first frame update
    void Start()
    {
        
        score = Director.GetScore();
        Text scoretext = GameObject.Find("ScorePoint").GetComponent<Text>();
        scoretext.text = "SCORE : " + score + "/10";

        Text Ev = GameObject.Find("Evaluation").GetComponent<Text>();

        if(score < 5)
        {
            Ev.text = "Not Bad";
        }
        else if(score >= 5 && score < 7)
        {
            Ev.text = "Good";
        }
        else if(score >= 7 && score <= 9)
        {
            Ev.text = "Excellent";
        }
        else
        {
            Ev.text = "Perfect!";
        }

    }

    public void GoTitle()
    {
        SceneManager.LoadScene("TITLE");
        score = 0;
    }
    public static int GetClearScore()
    {
        return score;
    }

}
