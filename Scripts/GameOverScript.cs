using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{

    public void GotoTitle()
    {
        SceneManager.LoadScene("TITLE");
    }

}
