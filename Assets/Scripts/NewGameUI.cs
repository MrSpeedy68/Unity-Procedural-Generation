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
        DBManager.isNewGame = false;
        //SceneManager.LoadScene(_sceneNumber Where Ended);
    }

    public void NewGame()
    {
        DBManager.isNewGame = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
