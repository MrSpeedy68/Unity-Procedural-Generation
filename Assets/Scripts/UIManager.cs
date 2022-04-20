using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text usernameText;
    public TMP_Text previousHighScoreText;


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
        
    }

    public void SaveScore()
    {
        DBManager.SaveScore();
        SceneManager.LoadScene(0);
    }
}
