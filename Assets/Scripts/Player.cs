using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float health = 100f;
    public int score = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        GetDBScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetDBScore()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 4)
        {
            if (DBManager.isNewGame)
            {
                score = 0;
            }
            else score = DBManager.currentScore;
        }
        else score = DBManager.currentScore;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            AddScore(50);
            Destroy(other.gameObject);
        }
    }

    private void AddScore(int amount)
    {
        score += amount;
        DBManager.currentScore = score;
    }
}
