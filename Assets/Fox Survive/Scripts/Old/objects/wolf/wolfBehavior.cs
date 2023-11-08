using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wolfBehavior : MonoBehaviour
{
    public float seeDistance = 5f;
    //дистанция до атаки
    public float attackDistance = 2f;
    //скорость енеми
    public float speed = 6;
    public float speedOfAttack = 18;
    public bool angry = true;
    //игрок
    private Transform target;
    private Rigidbody body;

    private bool attackState = false;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (!pause.Pause)
        {
            body.isKinematic = false;
            if (Vector3.Distance(transform.position, target.transform.position) < seeDistance && angry == true )
            {
                if (Vector3.Distance(transform.position, target.transform.position) > attackDistance)
                {
                    //walk
                    transform.LookAt(target.transform);
                    transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
                }
                else if (Vector3.Distance(transform.position, target.transform.position) > 0.7 && attackState == false)
                {
                    attackState = true;
                    transform.LookAt(target.transform);
                    body.AddForce(new Vector3(0, 0, speedOfAttack * Time.deltaTime), ForceMode.Impulse);
                    //transform.Translate(new Vector3(0, 0, speed * Time.deltaTime * 3));
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
                }
            }
        }
        else
        {
            body.isKinematic = true;
        }
        
    }
}
