using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class subInfo : MonoBehaviour
{
    public static GameObject subInfoObject;
    private static string[] info = new string[10];
    private void Start()
    {
        subInfoObject = gameObject;
        subInfo.addSubInfo(3, "Esc - open menu");
        subInfo.addSubInfo(4, "T - open skill menu");
    }
    private void Update()
    {
        refreshSubInfo();
    }
    public static void addSubInfo(int id, string data)
    {
        info[id] = data;
        refreshSubInfo();
    }
    public static void removeSubInfo(int id)
    {
        info[id] = null;
        refreshSubInfo();
    }
    private static void refreshSubInfo()
    {
        string s = "";
        foreach(string q in info)
        {
            if(q != null)
                s += "\n" + q;
        }
        subInfoObject.GetComponent<Text>().text = s;
    }
}
