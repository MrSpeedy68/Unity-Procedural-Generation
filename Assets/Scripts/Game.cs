using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    private string url = "https://elviveamten.000webhostapp.com/Savedata.php";
    public TMP_Text playerDisplay;
    public TMP_Text scoreDisplay;

    private void Awake()
    {
        if (DBManager.username == null)
        {
            SceneManager.LoadScene(0);
        }
        
        playerDisplay.text = "Player: " + DBManager.username;
        scoreDisplay.text = "Score: " + DBManager.score;
    }


    public void CallSaveData()
    {
        StartCoroutine(SavePlayerData());
    }

    IEnumerator SavePlayerData()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", DBManager.username);
        form.AddField("score", DBManager.score);

        WWW www = new WWW(url, form);
        yield return www;
        if (www.text == "0")
        {
            Debug.Log("Game Saved");
        }
        else
        {
            Debug.Log("Save Failed. Error #" + www.text);
        }
        
        DBManager.LogOut();
        SceneManager.LoadScene(0);
    }

    public void IncreaseScore()
    {
        DBManager.score++;
        scoreDisplay.text = "Score: " + DBManager.score;
    }
}
