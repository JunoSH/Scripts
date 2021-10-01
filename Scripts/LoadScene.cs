using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public GameObject Choose;

    void Start()
    {
        Choose.SetActive(false);
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
