using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bearGetDamage : MonoBehaviour
{
    // Start is called before the first frame update
    public float force = 300;
    public float radius = 4;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("1");
        if (col.gameObject.tag == "Player")
        {
            col.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, radius);
            Destroy(transform.parent.gameObject);
        }
    }
}
