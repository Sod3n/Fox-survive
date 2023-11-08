using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staticVariable : MonoBehaviour
{
    public static Vector3 lastPlayerPos;
    public static List<GameObject> Grounds_01;
    public static List<GameObject> Grounds_02;
    public static GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
