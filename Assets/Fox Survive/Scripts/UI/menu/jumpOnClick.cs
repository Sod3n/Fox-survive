using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpOnClick : MonoBehaviour
{
    public float jumpForce = 10;
    // Start is called before the first frame update
    void OnMouseDown()
    {
        GetComponent<Rigidbody>().velocity = new Vector2(0, jumpForce);
    }
}
