using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    private Position spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AI"))
        {
            Debug.Log("COLLION");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
