using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using System;

public class menuMethods : MonoBehaviour
{
    public GameObject camera;
    public GameObject panelMain;
    public GameObject panelSettings;
    public GameObject listOfSaves;
    public GameObject listOfLoads;
    public bool rotateToPanelSettings = false;
    public bool rotateToPanelMain = false;
    public bool isFullScreen;
    public UnityEngine.Audio.AudioMixer am;
    Resolution[] rsl;
    List<string> resolutions;
    public Dropdown dropdown;

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            if (rotateToPanelSettings)
            {
                camera.transform.Rotate(Vector3.down * Time.deltaTime * 70);
            }
            if (rotateToPanelMain)
            {
                camera.transform.Rotate(Vector3.up * Time.deltaTime * 70);
            }
            if (camera.transform.eulerAngles.y > 0 && camera.transform.eulerAngles.y < 7)
            {
                rotateToPanelMain = false;
                panelMain.SetActive(true);
            }
            if (camera.transform.eulerAngles.y > 267 && camera.transform.eulerAngles.y < 272)
            {
                rotateToPanelSettings = false;
                panelSettings.SetActive(true);
            }
        }
    }
    public void Awake()
    {
        resolutions = new List<string>();
        rsl = Screen.resolutions;
        foreach (var i in rsl)
        {
            resolutions.Add(i.width + "x" + i.height);
        }
        dropdown.ClearOptions();
        dropdown.AddOptions(resolutions);
        //DontDestroyOnLoad(this);
    }

    public void pressPlay()
    {
        SceneManager.LoadScene("Game");
    }
    public void pressSettings()
    {
        Debug.Log("F");
        panelMain.SetActive(false);
        if(SceneManager.GetActiveScene().name == "Menu")
        {
            rotateToPanelSettings = true;
        }
        else if(SceneManager.GetActiveScene().name == "Game")
        {
            panelSettings.SetActive(true);
            Debug.Log("Fx2");
        }
    }
    public void pressSave()
    {
        listOfSaves.SetActive(!listOfSaves.activeSelf);
    }
    public void changeListOfSaves(int s)
    {
        string path = Environment.CurrentDirectory;
        Directory.CreateDirectory(path);
        GameObject[] gameObjects = FindObjectsOfType<GameObject>();
        MyGameObjects[] myGameObjects = new MyGameObjects[gameObjects.Length];
        for(int i=0; i < gameObjects.Length; i++)
        {
            GameObject obj = gameObjects[i];
            if (obj.activeInHierarchy) 
            {
                MyGameObjects myGameObject;
                /*if (obj.name.Contains("Ground"))
                    myGameObject = new MyGameObjects(obj.transform.position, obj.transform.rotation, obj.name, obj.GetComponent<generateWorld>().increaseGen, obj.GetComponent<generateWorld>().genNewObj);
                else */
                if (obj.name.Contains("Player"))
                {
                    myGameObject = new MyGameObjects(obj.transform.position, obj.transform.rotation, obj.name, obj.GetComponent<needs>().health, obj.GetComponent<needs>().harvest);
                }
                else if (obj.tag == "storage_1")
                {
                    List<string> prefnames = new List<string>();
                    foreach (GameObject q in obj.GetComponent<storage>().storageObjects)
                    {
                        prefnames.Add(q.name);
                    }
                    myGameObject = new MyGameObjects(obj.transform.position, obj.transform.rotation, "storage_1", prefnames.ToArray());
                }
                else if(obj.tag == "enterHole")
                {
                    myGameObject = new MyGameObjects(obj.transform.position, obj.transform.rotation, obj.name, obj.GetComponent<holeState>().currentEnterHole);
                }
                else if (obj.transform.parent && obj.transform.parent.gameObject.name.Contains("mouth"))
                {
                    myGameObject = new MyGameObjects(obj.name, true);
                }
                else
                    myGameObject = new MyGameObjects(obj.transform.position, obj.transform.rotation, obj.name);
                //myGameObject.Initialize(gameObjects[i]);
                myGameObjects[i] = myGameObject;
            }
        }
        if(save.Save(path+s, myGameObjects))
        {
            Debug.Log("true");
        }
        
        listOfSaves.SetActive(false);
    }
    public void pressLoad()
    {
        listOfLoads.SetActive(!listOfLoads.activeSelf);
    }
    public void changeListOfLoads(int s)
    {
        save.Load(Environment.CurrentDirectory + s);
        SceneManager.LoadScene("Game");
    }

    public void pressBack()
    {
        panelSettings.SetActive(false);
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            rotateToPanelMain = true;
        }
        else if (SceneManager.GetActiveScene().name == "Game")
        {
            panelMain.SetActive(true);
        }
    }
    public void pressExit()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            Application.Quit();
        }
        else if (SceneManager.GetActiveScene().name == "Game")
        {
            SceneManager.LoadScene("Menu");
        }
    }
    public void FullScreenToggle()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
    }
    public void AudioVolume(float sliderValue)
    {
        am.SetFloat("masterVolume", sliderValue);
    }
    public void Quality(int q)
    {
        QualitySettings.SetQualityLevel(q);
    }
    public void Resolution(int r)
    {
        Screen.SetResolution(rsl[r].width, rsl[r].height, isFullScreen);
    }
}
