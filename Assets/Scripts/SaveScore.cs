using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveScore : MonoBehaviour
{
    // Start is called before the first frame update
    public string playerName;
    public int score;
    
    void Start()
    { 
        gameObject.GetComponent<InputField>().onEndEdit.AddListener(saveScore);
        score = 100;
    }

    private void saveScore(string textInField)
    {
        playerName = textInField;
        
        print("Starting to Save Score" +textInField);

        StartCoroutine(connectToPHP());
    }

    IEnumerator connectToPHP()
    {
        string url = "https://elviveamten.000webhostapp.com/UpdateScore.php";
        url += "?name=" + playerName + "&score=" + score;
        
        WWW www = new WWW(url);
        yield return www;
        print("DB has been updated");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
