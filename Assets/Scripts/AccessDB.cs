using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AccessDB : MonoBehaviour

{
    string url = "https://elviveamten.000webhostapp.com/UpdateScore.php";


    // Start is called before the first frame update

    IEnumerator Start()

    {
        WWW www = new WWW(url);
        yield return www;

        string result = www.text;

        print("Data Received " + result);

        GameObject.Find("high_scores").GetComponent<Text>().text = result;
    }
}