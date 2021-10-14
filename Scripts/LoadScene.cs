using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public GameObject Choose;
    int beforeScore;
    void Start()
    {
        Choose.SetActive(false);
        /*
        beforeScore = PlayerPrefs.GetInt("SCORE", 0);
        if (beforeScore > 0)
        {
            Text BeforeScore = GameObject.Find("PrefText").GetComponent<Text>();
            BeforeScore.text = "前回のスコア" + "-" + beforeScore.ToString() + "-";
        }
        */
    }

    public void ChooseQuiz()
    {
        Choose.SetActive(true);
    }
    public void CloseChoose()
    {
        Choose.SetActive(false);
    }
    public void GoGame()
    {
        SceneManager.LoadScene("QuizHontai");
    }
    public void GoGame2()
    {
        SceneManager.LoadScene("QuizHontai2");
    }

    public void GoTips()
    {
        SceneManager.LoadScene("Tips");
    }
}
