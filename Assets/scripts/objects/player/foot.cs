using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foot : MonoBehaviour
{
    public bool onGround = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag != "wolf" && col.gameObject.tag != "Player" && col.isTrigger == false) 
            onGround = true;
    }
    private void OnTriggerExit(Collider col)
    {
        //if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Ground_two")
            onGround = false;
    }
}
