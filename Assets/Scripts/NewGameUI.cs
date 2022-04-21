using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class NewGameUI : MonoBehaviour
{
    public void Continue()
    {
        if (PlayerPrefs.GetInt(DBManager.username + "scene") >= 4)
        {
            DBManager.isNewGame = false;
            DBManager.ContinueGame();
            SceneManager.LoadScene(PlayerPrefs.GetInt(DBManager.username+"scene"));
        }

    }

    public void NewGame()
    {
        DBManager.isNewGame = true;
        DBManager.currentScore = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
