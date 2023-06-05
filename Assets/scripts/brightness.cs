using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brightness : MonoBehaviour
{
    public createHole crHole;
    //public GameObject moon;
    public GameObject dayAndNightCircle;
    public GameObject directLight;
    public float speed = 5f;
    public bool blackout = false;
    public Material disCam;
    public Material Opa;
    public Canvas canvas;
    private Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        //moon = GameObject.Find("Moon");
        rend = GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(dayAndNightCircle.transform.localRotation.x);
        if ((dayAndNightCircle.transform.localRotation.x > 0.7f) && rend.material.color.a < 0.7f)
        {
            rend.material.color += new Color(0f, 0f, 0f, 0.01f * speed);
            directLight.SetActive(false);
        }
        /*else if(rend.material.color.a > 0.8f && !blackout)
        {
            rend.material.color -= new Color(0f, 0f, 0f, 0.01f * speed);
        }*/
        else if ((dayAndNightCircle.transform.localRotation.x < 0.5f) && rend.material.color.a > 0f)
        {
            directLight.SetActive(true);
            rend.material.color -= new Color(0f, 0f, 0f, 0.01f * speed);
        }
        if (Input.GetButtonDown("spawnHole"))
        {
            //blackout = true;
        }
        /*if (blackout && rend.material.color.a < 1f && crHole.canMakeHole)
        {
            rend.material.color += new Color(0f, 0f, 0f, 0.1f);
            crHole.canMakeHole = false;
            StartCoroutine(CoroutineWait());
        }*/
    }
    IEnumerator CoroutineWait()
    {
        yield return new WaitForSeconds(1f);
        canvas.enabled = false;
        rend.material = disCam;
        yield return new WaitForSeconds(4f);
        blackout = false;
        rend.material = Opa;
        canvas.enabled = true; 
    }
}
