using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reloadInventory : MonoBehaviour
{
    public GameObject content;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void relInvent()
    {
        content.GetComponent<generateCells>().reloadInventory();
    }
}
