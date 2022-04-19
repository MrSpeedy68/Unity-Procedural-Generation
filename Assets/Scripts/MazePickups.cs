using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePickups : MonoBehaviour
{
    public Transform[] spawnLocations;
    public GameObject pickup;

    public int spawnAmount;
    // Start is called before the first frame update
    void Start()
    {
        SpawnPickups();
    }

    private void SpawnPickups()
    {
        List<int> visited = new List<int>();

        while (spawnAmount > 0)
        {
            int rng = Random.Range(0, spawnLocations.Length);

            if (!visited.Contains(rng))
            {
                Instantiate(pickup, spawnLocations[rng].position, Quaternion.identity);
                spawnAmount--;
                visited.Add(rng);
            }
        }
    }
    
    
}
