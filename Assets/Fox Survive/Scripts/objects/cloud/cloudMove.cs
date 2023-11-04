using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudMove : MonoBehaviour
{
    public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!pause.Pause)
        {
            transform.position += Vector3.back * speed * Time.fixedDeltaTime;
        }
        
    }
}
