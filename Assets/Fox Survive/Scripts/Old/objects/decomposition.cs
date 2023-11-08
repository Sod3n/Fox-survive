using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class decomposition : MonoBehaviour
{
    public float maxhp;
    public float hp;
    public float weight;
    public Vector3 lastPos;
    public GameObject constituents;
    public generateWorld generateWorld;
    // Start is called before the first frame update
    void Start()
    {
        generateWorld = GameObject.FindGameObjectsWithTag("Ground")[0].GetComponent<generateWorld>();
    }
    void Update()
    {
        if (hp <= 0)
        {
            Instantiate(constituents);
            //generateWorld.addToStorage(gameObject);
        }
        if (hp > maxhp)
        {
            hp = maxhp;
        }
    }
    // Update is called once per frame
    void LateUpdate()
    {
        lastPos = transform.position;   
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<decomposition>())
        {
            decomposition decomposition = collision.gameObject.GetComponent<decomposition>();
            float speedEnergy = Mathf.Abs(collision.transform.position.x - decomposition.lastPos.x) + Mathf.Abs(collision.transform.position.z - decomposition.lastPos.z) + Mathf.Abs(collision.transform.position.y - decomposition.lastPos.y);
            hp -= decomposition.weight + speedEnergy;
        }
    }
}
