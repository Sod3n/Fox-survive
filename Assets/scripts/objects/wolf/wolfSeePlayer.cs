using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class wolfSeePlayer : MonoBehaviour
{
    //дистанция до атаки
    public float attackDistance = 2f;
    public int CDAttack = 2000;
    public int attackStun = 1000;
    //скорость енеми
    public float speed = 6;
    public bool angry = true;
    public GameObject wolf;
    public float speedOfAttack = 18;
    //игрок
    private Transform target;
    private Rigidbody body;
    public bool attackReady = true;
    public bool stun = false;

    public float speedOfPassiveRotation = 1;
    // Start is called before the first frame update
    void Start()
    {
        if (wolf.GetComponent<Rigidbody>())
            body = wolf.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!pause.Pause)
        {
            if (!stun)
            {
                if (target)
                {
                    if (angry == true)
                    {
                        if (attackReady)
                        {
                            if (Vector3.Distance(wolf.transform.position, target.transform.position) > attackDistance)
                            {
                                //walk
                                wolf.transform.LookAt(target.transform);
                                wolf.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
                                wolf.transform.rotation = Quaternion.Euler(0, wolf.transform.eulerAngles.y + 180, 0);
                            }
                            else if (Vector3.Distance(wolf.transform.position, target.transform.position) > 1)
                            {
                                wolf.transform.LookAt(target.transform);
                                Vector3 toOther = target.position - transform.position;
                                toOther.Normalize();
                                body.AddForce(toOther * speedOfAttack * Time.deltaTime, ForceMode.Impulse);
                                //wolf.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime * 3));
                                wolf.transform.rotation = Quaternion.Euler(0, wolf.transform.eulerAngles.y + 180, 0);
                                attackReady = false;
                                asyncStunAfterAttack();
                                asyncResetAttack();
                            }
                        }
                        else
                        {
                            wolf.transform.LookAt(target.transform);
                            wolf.transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
                            wolf.transform.rotation = Quaternion.Euler(0, wolf.transform.eulerAngles.y + 180, 0);
                        }
                    }
                }
                else
                {
                    wolf.transform.Rotate(new Vector3(0, speedOfPassiveRotation, 0));
                }
            }
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            target = col.transform;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            target = null;
        }
    }
    public async void asyncResetAttack()
    {
        await Task.Run(() => resetAttack());
    }
    public void resetAttack()
    {
        Thread.Sleep(CDAttack);
        attackReady = true;
    }
    public async void asyncStunAfterAttack()
    {
        await Task.Run(() => stunAfterAttack());
    }
    public void stunAfterAttack()
    {
        stun = true;
        Thread.Sleep(attackStun);
        stun = false;
    }
}
