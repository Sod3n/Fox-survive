using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class foot : MonoBehaviour
{
    public bool onGround = false;
    private bool supOnGround = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!supOnGround) onGround = false;
        supOnGround = false;
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag != "wolf" && col.gameObject.tag != "Player" && col.isTrigger == false)
        {
            onGround = true;
            supOnGround = true;
        }
            
    }
    private void OnTriggerExit(Collider col)
    {   
        onGround = false;
    }
}
