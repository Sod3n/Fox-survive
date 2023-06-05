using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hole : MonoBehaviour
{
    public createHole crHole;
    private Vector3 tr;
    private bool i = false;
    // Start is called before the first frame update
    void Start()
    {
        crHole = GameObject.FindGameObjectWithTag("Player").GetComponent<createHole>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(i)
       //     transform.position = tr;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground_two")
        {
            i = true;
            crHole.mode = 2;
            //tr = transform.position;
           // GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            StartCoroutine(CoroutineWait());
        }
    }
    IEnumerator CoroutineWait()
    {
        yield return new WaitForSeconds(2f);

        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        crHole.mode = 0;
        i = false;
    }
}
