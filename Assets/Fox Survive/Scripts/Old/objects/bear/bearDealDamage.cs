using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bearDealDamage : MonoBehaviour
{
    public needs need;
    public float damage;
    public float force = 300;
    public float radius = 4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            need = col.gameObject.GetComponent(typeof(needs)) as needs;
            need.GetDamage(damage);
            col.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, radius);
            Debug.Log("col");
        }
    }
}
