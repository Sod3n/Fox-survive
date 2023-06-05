using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("MyGameObjects")]
public class MyGameObjects
{
    //Default
    public Vector3 pos;
    public Quaternion rot;
    public string prefName;

    public MyGameObjects(Vector3 position, Quaternion rotation, string prefabName) 
    {
        pos = position;
        rot = rotation;
        prefName = prefabName;
        
    }

    //Ground
    public int increaseGen;
    public bool genObj;

    public MyGameObjects(Vector3 position, Quaternion rotation, string prefabName, int incGen, bool generateNewObjects)
    {
        pos = position;
        rot = rotation;
        prefName = prefabName;
        increaseGen = incGen;
        genObj = generateNewObjects;
    }

    //Player
    public float heal;
    public float harv;
    public MyGameObjects(Vector3 position, Quaternion rotation, string prefabName, float health, float harvest)
    {
        pos = position;
        rot = rotation;
        prefName = prefabName;
        heal = health;
        harv = harvest;
    }
    //inventory
    public string[] prefnames;
    public MyGameObjects(Vector3 position, Quaternion rotation, string prefabName, string[] prefabNames)
    {
        pos = position;
        rot = rotation;
        prefName = prefabName;
        prefnames = prefabNames;
    }
    //Hole
    public bool currentHole;
    public MyGameObjects(Vector3 position, Quaternion rotation, string prefabName, bool curHole)
    {
        pos = position;
        rot = rotation;
        prefName = prefabName;
        currentHole = curHole;
    }
    public bool pickedUp = false;
    public MyGameObjects(string prefabName, bool pickUp)
    {
        prefName = prefabName;
        currentHole = pickUp;
    }
    public MyGameObjects() { }
    /*public void Initialize(GameObject go)
    {
        pos = go.transform.position;
        eulerAngles = go.transform.eulerAngles;
        prefabName = go.name;
    }*/
}
