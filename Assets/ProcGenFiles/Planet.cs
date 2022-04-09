using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    private string name { get; set; }
    private float diameter;
    private float distanceToSun = 150;
    private float rotationPeriod = 10f;
    private float orbitalVelocity = .20f;
    private Color color = new Color(1.0f,1.0f,1.0f);
    
    
    float orbitalAngle = 0.0f;
    //float angle = 0.0f;
    //float orbitalRotationalSpeed = 20f;

    //private Color c1 = color;
    int lengthOfLineRenderer = 100;

    GameObject sun;

    public void CreatePlanet(string newName, float newDiameter, float newDistanceToSun, float newRotationPeriod, float newOrbitalVelocity, Color newColor)
    {
        name = newName;
        diameter = newDiameter * 10;
        distanceToSun = newDistanceToSun * 75;
        rotationPeriod = newRotationPeriod;
        orbitalVelocity = newOrbitalVelocity;
        color = newColor;
    }  

    void DrawOrbit()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));
        lineRenderer.SetColors(color, color);
        lineRenderer.SetWidth(1.0f, 1.0f);
        lineRenderer.SetVertexCount(lengthOfLineRenderer + 1);

        int i = 0;
        while (i <= lengthOfLineRenderer)
        {
            float unitAngle = (float)(2 * 3.14) / lengthOfLineRenderer;
            float currentAngle = (float)unitAngle * i;

            Vector3 pos = new Vector3(distanceToSun * Mathf.Cos(currentAngle), 0,
                distanceToSun * Mathf.Sin(currentAngle));
            lineRenderer.SetPosition(i, pos);
            i++;
        }
    }


    void Start()
    {
        sun = GameObject.Find("sun");
        transform.position = new Vector3(distanceToSun, 0, distanceToSun);
        transform.localScale = new Vector3(diameter, diameter, diameter);
        transform.name = name;

        var Mat = GetComponent<Renderer>();
        Mat.material.color = color;
        
        DrawOrbit();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationPeriod * Time.deltaTime, Space.World);
        float tempx, tempy, tempz;
        orbitalAngle += Time.deltaTime * orbitalVelocity;
        tempx = sun.transform.position.x + distanceToSun * Mathf.Cos(orbitalAngle);
        tempz = sun.transform.position.z + distanceToSun * Mathf.Sin(orbitalAngle);
        tempy = sun.transform.position.y;
        transform.position = new Vector3(tempx, tempy, tempz);
    }
}