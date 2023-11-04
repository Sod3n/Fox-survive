using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudTrigger : MonoBehaviour
{
    public bool px = false;
    public bool py = false;
    public bool nx = false;
    public bool ny = false;
    public float xRange = 1000f;
    public float yRange = 1000f;
    private float cxRange;
    private float cyRange;
    // Start is called before the first frame update
    void Start()
    {
        if (px)
        {
            cxRange = xRange;
        }
        else if (nx)
        {
            cxRange = -xRange;
        }
        if (py)
        {
            cyRange = yRange;
        }
        else if (ny)
        {
            cyRange = -yRange;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Cloud")
        {
            Vector3 newPos = new Vector3();
            newPos.x = cxRange;
            newPos.y = cyRange;
            col.transform.position = transform.position + newPos;
        }
    }
}
