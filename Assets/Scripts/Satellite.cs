using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : MonoBehaviour
{
    public Transform target;
    public float orbitDistance = 10.0f;
    public float orbitDegreesPerSec = 180.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Orbit();
    }
    
    void Orbit()
    {
        if(target != null)
        {
            // Keep us at orbitDistance from target
            transform.position = target.position + (transform.position - target.position).normalized * orbitDistance;
            transform.RotateAround(target.position, Vector3.up, orbitDegreesPerSec * Time.deltaTime);
        }
    }
}
