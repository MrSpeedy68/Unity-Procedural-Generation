using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickupManager : MonoBehaviour
{
    
    private bool startCheck = false;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FindObjects());
    }
    
    void Update()
    {
        if (startCheck)
        {
            if (GameObject.FindGameObjectsWithTag("Pickup").Length <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    IEnumerator FindObjects()
    {
        yield return new WaitForSeconds(5f);

        startCheck = true;
        
        yield return null;
    }
}
