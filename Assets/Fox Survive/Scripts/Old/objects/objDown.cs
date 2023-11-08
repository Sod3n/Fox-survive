using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objDown : MonoBehaviour
{
    private Rigidbody rb;
    public bool moved = false;
    private bool checkFall = true;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        fall();
    }

    // Update is called once per frame
    void Update()
    {
        if (checkFall)
        {
            if (transform.position.y < -1000)
            {
                gameObject.SetActive(false);
            }
        }
       
    }
    void OnCollisionStay(Collision collision)
    {
        
        if ((collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Ground_two") && GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().Sleep();
            GetComponent<Rigidbody>().isKinematic = true;
            moved = false;
            checkFall = false;
        }
        else if ((GetComponent<Rigidbody>().isKinematic == false && collision.gameObject.tag == "Player") || moved)
        {
            transform.position += Vector3.forward;
            moved = false;
            Debug.Log("Sleep");
        }
        else if(GetComponent<Rigidbody>().isKinematic == false)
            gameObject.SetActive(false);
            

    }
    public void fall()
    {
        /*Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.drag = -2;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
        GetComponent<MeshCollider>().convex = true;*/
        GetComponent<Rigidbody>().WakeUp();
        GetComponent<Rigidbody>().isKinematic = false;
        checkFall = true;
    }
}
