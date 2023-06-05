using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public class save : MonoBehaviour
{
    public static MyGameObjects[] MyGameObjects = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public static bool Save(string datapath, MyGameObjects[] gameObjects)
    {
        //Type[] extraTypes = { typeof(PositData), typeof(Lamp) };
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(MyGameObjects[]));

            FileStream fs = new FileStream(datapath, FileMode.Create);

            //GameObject[] gameObjects = Object.FindObjectsOfType<GameObject>();
            serializer.Serialize(fs, gameObjects);
            fs.Close();
            //fs = new FileStream(datapath, FileMode.Open);
            //Debug.Log((string)serializer.Deserialize(fs));
            return true;
        }
        catch(IOException x)
        {
            return false;
        }
    }
    public static MyGameObjects[] Load(string datapath)
    {
        //Type[] extraTypes = { typeof(PositData), typeof(Lamp) };
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(MyGameObjects[]));

            FileStream fs = new FileStream(datapath, FileMode.Open);

            //GameObject[] gameObjects = Object.FindObjectsOfType<GameObject>();
            MyGameObjects[] gameObjects = (MyGameObjects[])serializer.Deserialize(fs);
            fs.Close();
            //fs = new FileStream(datapath, FileMode.Open);
            //Debug.Log((string)serializer.Deserialize(fs));
            MyGameObjects = gameObjects;
            return gameObjects;
        }
        catch (IOException x)
        {
            return null;
        }
    }
}