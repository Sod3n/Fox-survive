using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wolfGetDamage : MonoBehaviour
{
    // Start is called before the first frame update
    public float force = 100;
    public GameObject dieParticle;
    public GameObject loot;
    public GameObject wolf;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider col)
    {
        //Debug.Log("1");
        if (col.gameObject.tag == "Player")
        {
            col.GetComponent<Rigidbody>().AddForce(force * transform.up);
            dieParticle = Instantiate(dieParticle);
            dieParticle.transform.position = transform.position;
            dieParticle.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
            dieParticle.SetActive(true);
            statistics.wolfsKilled++;
            foreach (Transform q in loot.transform)
            {
                q.gameObject.SetActive(true);
                q.parent = null;
            }
            Destroy(wolf);
        }
    }
}
