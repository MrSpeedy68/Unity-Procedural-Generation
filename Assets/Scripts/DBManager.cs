using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DBManager
{
    public static string username;
    public static int score;
    public static bool isNewGame;
    public static int currentScore;
    
    private static string url = "https://elviveamten.000webhostapp.com/Savedata.php";
    
    public static bool LoggedIn
    {
        get { return username != null; }
    }

    public static void LogOut()
    {
        username = null;
    }

    public static void ContinueGame()
    {
        currentScore = PlayerPrefs.GetInt(username);
    }
}
