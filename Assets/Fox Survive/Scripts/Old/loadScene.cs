using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadScene : MonoBehaviour
{
    public GameObject Ground_01;
    public GameObject Ground_02;
    public GameObject Tree_01;
    public GameObject Tree_02;
    public GameObject Rock_01;
    public GameObject Rock_02;
    public GameObject Rock_03;
    public GameObject Rock_04;
    public GameObject Mushroom_01;
    public GameObject Mushroom_02;
    public GameObject Bush_01;
    public GameObject Bush_02;
    public GameObject Grass_01;
    public GameObject Grass_02;
    public GameObject Stump_01;
    public GameObject Branch_01;
    public GameObject Wolf;
    public GameObject Cube;
    public GameObject ExitHole;
    public GameObject EnterHole;
    public GameObject Spawn;
    public GameObject DayAndNight;
    public GameObject Clouds;
    public GameObject MainCamera;
    public GameObject BreakedHole;

    public GameObject storage_1;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        MyGameObjects[] myGameObjects = save.MyGameObjects;
        pause.Pause = true;
        if (save.MyGameObjects != null)
        {
            for(int i = 0; i < myGameObjects.Length; i++)
            {
                if (findGameObjectByName(myGameObjects[i].prefName) != null)
                {
                    GameObject go;
                    if (defaultObjects(myGameObjects[i].prefName))
                    {
                        go = GameObject.Find(myGameObjects[i].prefName);
                        go.transform.position = myGameObjects[i].pos;
                        go.transform.rotation = myGameObjects[i].rot;
                        if (myGameObjects[i].prefName.Contains("Player"))
                        {
                            go.GetComponent<needs>().health = myGameObjects[i].heal;
                            go.GetComponent<needs>().harvest = myGameObjects[i].harv;
                            //go.transform.Find("mouth").GetComponent<createHole>().enterHole = myGameObjects[i].
                        }
                        Debug.Log(myGameObjects[i].prefName);
                    }
                    else if (myGameObjects[i].pickedUp)
                    {
                        go = Instantiate(findGameObjectByName(myGameObjects[i].prefName));
                        Player.transform.Find("mouth").GetComponent<thingsInteract>().pickUp(go);
                    }
                    else
                    {
                        go = Instantiate(findGameObjectByName(myGameObjects[i].prefName), myGameObjects[i].pos, myGameObjects[i].rot);

                        if (myGameObjects[i].prefName.Contains("Ground"))
                        {
                            //go.GetComponent<generateWorld>().increaseGen = myGameObjects[i].increaseGen;
                            //go.GetComponent<generateWorld>().genNewObj = myGameObjects[i].genObj;
                        }
                        if(go.tag == EnterHole.tag)
                        {
                            if (myGameObjects[i].currentHole)
                            {
                                Player.transform.Find("mouth").GetComponent<createHole>().enterHole = go;
                                go.GetComponent<holeState>().currentEnterHole = true;
                            }
                        }
                    }
                    
                }
                else if (myGameObjects[i].prefName == "storage_1")
                {
                    foreach(string q in myGameObjects[i].prefnames)
                    {
                        GameObject go = Instantiate(findGameObjectByName(q), new Vector3(), new Quaternion());
                        go.SetActive(false);
                        storage_1.GetComponent<storage>().storageObjects.Add(go);
                        storage_1.GetComponent<storage>().curCount++;
                    }
                }
            }
        }
        else
        {
            GameObject go = Instantiate(Ground_01, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            //go.GetComponent<generateWorld>().genNewObj = true;
            /*Instantiate(Player, new Vector3(0, 10, 0), new Quaternion(0, 0, 0, 0));
            Instantiate(Clouds, new Vector3(35, 25, -465), new Quaternion(0, 0, 0, 0));
            Instantiate(DayAndNight, new Vector3(54, 4, 0), new Quaternion(0, 0, 0, 0));
            Instantiate(Spawn, new Vector3(0, 10, 0), new Quaternion(0, 0, 0, 0));
            Instantiate(Cube, new Vector3(0, -100, 0), new Quaternion(0, 0, 0, 0));
            Instantiate(MainCamera, new Vector3(54, 4, 0), new Quaternion(0, 0, 0, 0));*/
        }
        pause.Pause = false;
    }
    public GameObject findGameObjectByName(string name)
    {
        if(name != null)
        {
            if (name.Contains("Branch_01"))
                return Branch_01;
            if (name.Contains("Bush_01"))
                return Bush_01;
            if (name.Contains("Bush_02"))
                return Bush_02;
            if (name.Contains("Grass_01"))
                return Grass_01;
            if (name.Contains("Grass_02"))
                return Grass_02;
            if (name.Contains("Ground_01"))
                return Ground_01;
            if (name.Contains("Ground_02"))
                return Ground_02;
            if (name.Contains("Mushroom_01"))
                return Mushroom_01;
            if (name.Contains("Mushroom_02"))
                return Mushroom_02;
            if (name.Contains("Rock_01"))
                return Rock_01;
            if (name.Contains("Rock_02"))
                return Rock_02;
            if (name.Contains("Rock_03"))
                return Rock_03;
            if (name.Contains("Rock_04"))
                return Rock_04;
            if (name.Contains("Stump_01"))
                return Stump_01;
            if (name.Contains("Tree_01"))
                return Tree_01;
            if (name.Contains("Tree_02"))
                return Tree_02;
            if (name.Contains("Wolf"))
                return Wolf;
            if (name.Contains("Player"))
                return Player;
            if (name.Contains("EnterHole"))
                return EnterHole;
            if (name.Contains("Spawn"))
                return Spawn;
            if (name.Contains("DayAndNight"))
                return DayAndNight;
            if (name.Contains("Clouds"))
                return Clouds;
            if (name.Contains("BreakedHole"))
                return BreakedHole;
        }
       
        return null;
    }
    public bool defaultObjects(string name)
    {
        
        if (name.Contains("Player"))
            return true;
        if (name.Contains("Cube"))
            return true;
        if (name.Contains("DayAndNight"))
            return true;
        if (name.Contains("MainCamera"))
            return true;
        if (name.Contains("Spawn"))
            return true;
        if (name.Contains("exitHole"))
            return true;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
