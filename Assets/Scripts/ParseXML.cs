using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class ParseXML : MonoBehaviour
{
    private XmlDocument itemDataXml;

    public GameObject planet;

    private Planet _Planet;
    void Start()
    {
        TextAsset xmlTextAsset = Resources.Load<TextAsset>("planets");
        if (xmlTextAsset != null)
        {
            itemDataXml = new XmlDocument();
            itemDataXml.LoadXml(xmlTextAsset.text);
            
            XmlNodeList nodeList;
            XmlNode root = itemDataXml.DocumentElement;

            nodeList = root.SelectNodes("descendant::planet");

            for (int i = 0; i < nodeList.Count; i++)
            {
                print(nodeList[i].Attributes["name"].Value);
                print(nodeList[i].Attributes["diameter"].Value);
                print(nodeList[i].Attributes["distancetoSun"].Value);
                print(nodeList[i].Attributes["rotationPeriod"].Value);
                print(nodeList[i].Attributes["orbitalVelocity"].Value);

                string name = nodeList[i].Attributes["name"].Value;
                float diam = float.Parse(nodeList[i].Attributes["diameter"].Value);
                float distToSun = float.Parse(nodeList[i].Attributes["distancetoSun"].Value);
                float rotPeriod = float.Parse(nodeList[i].Attributes["rotationPeriod"].Value);
                float orbVelocity = float.Parse(nodeList[i].Attributes["orbitalVelocity"].Value);
                
                CreatePlanet(name, diam,distToSun,rotPeriod,orbVelocity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreatePlanet(string aName, float aDiameter, float aDistancetoSun, float aRotationPeriod, float aOrbitalVelocity)
    {
        var p = Instantiate(planet, transform.position, Quaternion.identity);
        _Planet = p.GetComponent<Planet>();

       _Planet.CreatePlanet(aName, aDiameter, aDistancetoSun, aRotationPeriod, aOrbitalVelocity);
    }

    // private void LoadData()
    // {
    //     name = itemDataXml.
    // }
    
}
