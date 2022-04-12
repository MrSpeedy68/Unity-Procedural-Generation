using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public GameObject[] pickups;
    private bool startCheck = false;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FindObjects());
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (startCheck)
        {
            int j = 0;
            for(int i = 0; i < pickups.Length; i++)
            {
                if (pickups[i] == null)
                {
                    j++;
                }

                if (j == pickups.Length)
                {
                    Debug.Log("Ur Done");
                }
            } 
        }
    }

    IEnumerator FindObjects()
    {
        yield return new WaitForSeconds(0.5f);
        
        pickups = GameObject.FindGameObjectsWithTag("Pickup");

        startCheck = true;
        
        yield return null;
    }
}
