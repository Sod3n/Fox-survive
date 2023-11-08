using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dayCircle : MonoBehaviour
{
    public float speed;
    public Light directLight;
    public GameObject playerCamera;
    public GameObject stars;
    public Color darkColor;
    public Color yellowColor;
    public float speedOfLightChange = 0.1f;
    public Material cloudsMaterial;

    private Color lightColor;
    private Color curColor;
    private Color lastColor;
    private float rotx = 0;
    private float cr;
    private float cb;
    private float cg;
    private float curI = 1;
    private Coroutine curColorSet;


    void Start()
    {
        lightColor = playerCamera.GetComponent<Camera>().backgroundColor;
        curColor = lightColor;
        lastColor = lightColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause.Pause)
        {
            #if UNITY_EDITOR
        cloudsMaterial = cloudSpawner.cloudMaterial;
#endif
            transform.Rotate(speed * Time.deltaTime, 0, 0);
            rotx += speed * Time.deltaTime;
            if(rotx >= 180)
            {
                rotx -= 360;
            }
            if (rotx > 90 && lastColor == yellowColor)
            {
                //directLight.SetActive(false);
                lightSet(0);
                setColorImplement(darkColor);
                startColorSetCoroutine();
                stars.SetActive(true);
                Color clcol = cloudsMaterial.GetColor(Shader.PropertyToID("_AmbientColor"));
                clcol.a = 0.3f;
                cloudsMaterial.SetColor(Shader.PropertyToID("_AmbientColor"), clcol);
                //playerCamera.GetComponent<Camera>().backgroundColor = darkColor;

            }
            else if (rotx > -90 && rotx < 80f && lastColor == yellowColor)
            {
                //directLight.SetActive(true);
                lightSet(1);
                setColorImplement(lightColor);
                startColorSetCoroutine();
                //playerCamera.GetComponent<Camera>().backgroundColor = lightColor;#6D84A800

            }
            if ((rotx > -100 && rotx < -90 && lastColor == darkColor) || (rotx > 80f && lastColor == lightColor))
            {
                stars.SetActive(false);
                Color clcol = cloudsMaterial.GetColor(Shader.PropertyToID("_AmbientColor"));
                clcol.a = 1;
                cloudsMaterial.SetColor(Shader.PropertyToID("_AmbientColor"), clcol);
                lightSet(0.5f);
                setColorImplement(yellowColor);
                startColorSetCoroutine();
            }
            //Debug.Log(rotx);
            //Debug.Log(lastColor == lightColor);
        }
            
    }
    private void startColorSetCoroutine()
    {
        if (curColorSet != null)
            StopCoroutine(curColorSet);
        curI = 1;
        curColorSet = StartCoroutine(colorSet());
    }
    private void lightSet(float light)
    {
        if(directLight.intensity > light)
        {
            StartCoroutine(lightDown(light));
        }
        else
        {
            StartCoroutine(lightUp(light));
        }
    }
    IEnumerator lightUp(float light)
    {
        yield return new WaitForSeconds(0.1f);
        if(directLight.intensity < light)
        {
            directLight.intensity += speedOfLightChange;
            StartCoroutine(lightUp(light));
        }
        
    }
    IEnumerator lightDown(float light)
    {
        yield return new WaitForSeconds(0.1f);
        if (directLight.intensity > light)
        {
            directLight.intensity -= speedOfLightChange;
            StartCoroutine(lightDown(light));
        }
    }
    

    IEnumerator colorSet()
    {
        yield return new WaitForSeconds(0.1f);
        if (curI > 0)
        {
            curI -= speedOfLightChange;
            curColor = new Color(curColor.r - cr, curColor.g - cg, curColor.b - cb);
            playerCamera.GetComponent<Camera>().backgroundColor = curColor;
            curColorSet = StartCoroutine(colorSet());
        }
        else
        {
            curI = 1;
        }

    }
    private void setColorImplement(Color needColor)
    {
        lastColor = needColor;
        cr = (curColor.r - needColor.r) / (1 / (speedOfLightChange)); //30 - 10 /  1/0.0001
        cb = (curColor.b - needColor.b) / (1 / (speedOfLightChange));
        cg = (curColor.g - needColor.g) / (1 / (speedOfLightChange));
    }

}
