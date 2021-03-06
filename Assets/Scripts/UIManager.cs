using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text usernameText;
    public TMP_Text previousHighScoreText;

    public GameObject scoreUI;
    public GameObject pauseUI;

    private void Start()
    {
        pauseUI.SetActive(false);
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (scoreText)
        {
            scoreText.text = "Score: " + DBManager.currentScore;
        }
        
        if (usernameText)
        {
            usernameText.text = "User: " + DBManager.username;
        }

        if (previousHighScoreText)
        {
            previousHighScoreText.text = "Previous High Score: " + DBManager.score.ToString();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseUI && scoreUI) ChangeUI();
        }

    }

    void ChangeUI()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);
        scoreUI.SetActive(!scoreUI.activeSelf);

        if (scoreUI.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            
        }

        
    }

    public void ContinueGame()
    {
        ChangeUI();
    }

    public void SaveScore() //Exiting UI Should save the score in PlayerPrefs.
    {
        //DBManager.SaveScore();
        PlayerPrefs.SetInt(DBManager.username, DBManager.currentScore);
        PlayerPrefs.SetInt(DBManager.username + "scene", SceneManager.GetActiveScene().buildIndex);
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(0);
    }

    public void SaveScoreDB()
    {
        StartCoroutine(SavePlayerData());
    }
    
    private string url = "https://elviveamten.000webhostapp.com/Savedata.php";

    IEnumerator SavePlayerData()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", DBManager.username);
        form.AddField("score", DBManager.currentScore);
        
        Debug.Log("Save Data " + DBManager.username +"   " + DBManager.currentScore);

        WWW www = new WWW(url, form);
        yield return www;
        
        Debug.Log(www.text);
        if (www.text == "0")
        {
            Debug.Log("Game Saved");
        }
        else
        {
            Debug.Log("Save Failed. Error #" + www.text.ToString());
        }
        
        DBManager.LogOut();
        SceneManager.LoadScene(0);
    }
}
