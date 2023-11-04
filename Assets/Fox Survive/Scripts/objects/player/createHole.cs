using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createHole : MonoBehaviour
{
    public GameObject hole;
    public GameObject camera;
    public GameObject enterHole;
    public GameObject exitHole;
    public GameObject spawn;
    public brightness brightness;
    public GameObject player;
    public float mode = 0;
    public bool canMakeHole = false;
    Ray ray;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerStay(Collider col)
    {
        if (!pause.Pause)
        {
            /*if (Input.GetButtonDown("Eat"))
            {
                if(col.gameObject.CompareTag("BreakedHole"))
                {
                    GameObject go = Object.Instantiate(hole, col.gameObject.transform.position, col.gameObject.transform.rotation);
                    go.GetComponent<holeState>().currentEnterHole = true;
                    col.gameObject.SetActive(false);
                    enterHole.GetComponent<holeState>().currentEnterHole = false;
                }
                else if (col.gameObject.CompareTag("enterHole"))
                {
                    StartCoroutine(enterInHole(col));
                }
                else if (col.gameObject.CompareTag("exitHole"))
                    StartCoroutine(exitFromHole());
            }*/
            if (col.gameObject.CompareTag("BreakedHole"))
            {
                subInfo.addSubInfo(0, "E - create hole");
                if (Input.GetButtonDown("Eat"))
                {
                    if(player.GetComponent<needs>().harvest > 75)
                    {
                        player.GetComponent<needs>().harvest -= 75;
                        Transform h = col.transform.Find("BreakedHole");
                        GameObject go = Object.Instantiate(hole, h.position, h.rotation);
                        go.GetComponent<holeState>().currentEnterHole = true;
                        col.gameObject.SetActive(false);
                        enterHole.GetComponent<holeState>().currentEnterHole = false;
                        statistics.dugHoles++;
                    }
                }
            }
            else if (col.gameObject.CompareTag("enterHole"))
            {
                subInfo.addSubInfo(0, "E - enter in burrow");
                if (Input.GetButtonDown("Eat"))
                {
                    player.layer = 3;
                    StartCoroutine(enterInHole(col));
                }
            }
            else if (col.gameObject.CompareTag("exitHole"))
            {
                subInfo.addSubInfo(0, "E - exit from burrow");
                if (Input.GetButtonDown("Eat"))
                {
                    player.layer = 8;
                    StartCoroutine(exitFromHole());
                }
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("exitHole") || col.gameObject.CompareTag("BreakedHole") || col.gameObject.CompareTag("exitHole"))
        {
            subInfo.removeSubInfo(0);
        }
    }
    IEnumerator enterInHole(Collider col)
    {
        yield return new WaitForSeconds(0.1f);
        player.transform.position = exitHole.transform.position - Vector3.forward;
        enterHole = col.gameObject;
    }
    IEnumerator exitFromHole()
    {
        yield return new WaitForSeconds(0.1f);
        player.transform.position = enterHole.transform.position + Vector3.up;
    }
}
