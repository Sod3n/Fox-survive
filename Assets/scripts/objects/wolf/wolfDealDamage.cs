using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wolfDealDamage : MonoBehaviour
{
    private needs need;
    public float damage;
    public float force = 300;
    public GameObject seeRadius;
    private wolfSeePlayer wolfSeePlayer;
    // Start is called before the first frame update
    void Start()
    {
        wolfSeePlayer = seeRadius.GetComponent<wolfSeePlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player" && wolfSeePlayer.angry)
        {
            need = col.gameObject.GetComponent(typeof(needs)) as needs;
            need.GetDamage(damage);
            col.GetComponent<Rigidbody>().AddForce(-transform.position.normalized * force, ForceMode.Force);
            Debug.Log("col");
        }
    }
}
