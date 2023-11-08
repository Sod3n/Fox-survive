using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thingsInteract : MonoBehaviour
{
    public needs need;
    public objDown objdown;
    public float mushroomSatiety = 20f;
    public float amanitaHeal = 20f;
    public float strength = 1;
    public bool die = false;
    public float pickUpedObjWidth = 20;
    public float pickUpedObjHeight = 20;
    public float pickUpedObjLength = 20;
    public GameObject player;
    public GameObject spaceMagSphere;
    private bool pickUping = false;
    private Vector3 pickUpedObjScale;
    public GameObject pickUpedObj = null;
    public Shader lightingShader;
    [Range(0, 1f)]
    public float lightingStrength = 0.2f;
    private Shader standartShader;
    public GameObject enterObj;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause.Pause) 
        {
            if (pickUpedObj != null)
                pickUpedObj.transform.position = transform.position;
            if (Input.GetButtonDown("pickUp") || die)
            {
                removeObjFromHands();
            }
        }
        if (!pause.Pause && !pickUping)
        {
            if (enterObj != null && pickUpedObj == null)
            {

                if (enterObj.gameObject.tag == "Mushroom")
                {
                    subInfo.addSubInfo(0, "E - eat");
                    if (Input.GetButtonDown("Eat"))
                    {
                        need.Eat(mushroomSatiety);
                        statistics.foodshrumsEated++;
                        Destroy(enterObj.gameObject);
                    }
                }
                if (enterObj.gameObject.tag == "Amanita")
                {
                    subInfo.addSubInfo(0, "E - eat");
                    if (Input.GetButtonDown("Eat"))
                    {
                        need.Heal(amanitaHeal);
                        statistics.amanitsEated++;
                        Destroy(enterObj.gameObject);
                    }
                }
                if (Input.GetButtonDown("pickUp"))
                {
                    if (enterObj != null && pickUpedObj == null)
                    {
                        pickUping = true;
                        StartCoroutine(CoroutineWait());
                    }
                }
                subInfo.addSubInfo(1, "F - pick up object");
            }
        }

    }
    void OnTriggerStay(Collider collider)
    {
        
        
    }
    public void pickUp(GameObject gameObject)
    {
        enterObj = gameObject;
        pickUping = true;
        StartCoroutine(CoroutineWait());
    }
    void OnTriggerEnter(Collider collider)
    {
        if(enterObj == null && canPickUp(collider.gameObject.tag))
        {
            enterObj = collider.gameObject;
            standartShader = enterObj.GetComponent<Renderer>().material.shader;
            enterObj.GetComponent<Renderer>().material.shader = lightingShader;
            enterObj.GetComponent<Renderer>().material.SetFloat("_LightingStrength", lightingStrength);
        }
            
    }
    void OnTriggerExit(Collider collider)
    {
        if (enterObj != null && canPickUp(collider.gameObject.tag))
        {
            enterObj.GetComponent<Renderer>().material.shader = standartShader;
            enterObj = null;
            subInfo.removeSubInfo(0);
            subInfo.removeSubInfo(1);
        }
            
    }
    public void removeObjFromHands()
    {
        if (pickUpedObj != null)
        {
            spaceMagSphere.SetActive(false);
            pickUpedObj.transform.parent = null;
            pickUpedObj.transform.rotation = player.transform.rotation;
            pickUpedObj.transform.localScale = pickUpedObjScale;
            Debug.Log("pickUpedObjScale: " + pickUpedObjScale);
            if (pickUpedObj.GetComponent<MeshCollider>())
                pickUpedObj.GetComponent<MeshCollider>().enabled = true;
            if (pickUpedObj.GetComponent<BoxCollider>())
                pickUpedObj.GetComponent<BoxCollider>().enabled = true;
            pickUpedObj.transform.Translate(Vector3.back);
            objdown = pickUpedObj.GetComponent(typeof(objDown)) as objDown;
            objdown.moved = true;
            objdown.fall();
            pickUpedObj = null;
            if(enterObj.GetComponent<Renderer>())
                enterObj.GetComponent<Renderer>().material.shader = standartShader;
            enterObj = null;
        }
    }
    public bool canPickUp(string name)
    {
        switch (name)
        {
            case "Mushroom":
                if (strength > 0)
                    return true;
                else
                    return false;
            case "Amanita":
                if (strength > 0)
                    return true;
                else
                    return false;
            case "branch":
                if (strength > 0)
                    return true;
                else
                    return false;
            case "rock_2":
                if (strength > 0)
                    return true;
                else
                    return false;
            case "rock_1":
                if (strength > 1)
                    return true;
                else
                    return false;
            case "rock_3":
                if (strength > 2)
                    return true;
                else
                    return false;
            case "rock_4":
                if (strength > 3)
                    return true;
                else
                    return false;
            case "bush":
                if (strength > 1)
                    return true;
                else
                    return false;
            case "grass":
                if (strength > 0)
                    return true;
                else
                    return false;
            case "stump":
                if (strength > 2)
                    return true;
                else
                    return false;
            case "tree":
                if (strength > 4)
                    return true;
                else
                    return false;
        }
        return false;
    }
    IEnumerator CoroutineWait()
    {
        yield return new WaitForSeconds(0f);
        try
        {
            spaceMagSphere.SetActive(true);
            pickUpedObj = enterObj;
            if (pickUpedObj.GetComponent<MeshRenderer>())
            {
                MeshRenderer mesh = pickUpedObj.GetComponent<MeshRenderer>();
                Vector3 size = mesh.bounds.size;
                Vector3 scale = pickUpedObj.transform.localScale;
                scale = toSizeOfAll(scale, size);
                pickUpedObj.transform.localScale = scale;
                if (pickUpedObj.GetComponent<MeshCollider>())
                    pickUpedObj.GetComponent<MeshCollider>().enabled = false;
                if (pickUpedObj.GetComponent<BoxCollider>())
                    pickUpedObj.GetComponent<BoxCollider>().enabled = false;
                if (pickUpedObj.GetComponent<CapsuleCollider>())
                    pickUpedObj.GetComponent<CapsuleCollider>().enabled = false;
                pickUpedObj.transform.SetParent(transform, true);
                pickUpedObj.SetActive(true);
                pickUpedObj.transform.localRotation = Quaternion.Euler(0, 0, 0);
                pickUpedObj.transform.localPosition = new Vector3(-size.x/2, -size.y / 2, -size.z / 2);

            }
            
        }
        catch(Exception er)
        {
            Debug.Log(er.ToString());
        }
        pickUping = false;
    }
    private Vector3 toSizeOfAll(Vector3 scale, Vector3 size)
    {
        
        Debug.Log(pickUpedObj.transform.localScale);
        pickUpedObjScale = scale;
        float fcof = size.x / scale.x;
        scale.x = fcof;
        fcof = pickUpedObjLength / 100 / fcof;
        float scof = size.y / scale.y;
        scale.y = fcof;
        scof = pickUpedObjHeight / 100 / scof;
        float tcof = size.z / scale.z;
        scale.z = fcof;
        tcof = pickUpedObjWidth / 100 / tcof;
        float[] c = { fcof, scof, tcof };
        float cof = Mathf.Min(c);
        return new Vector3(cof, cof, cof);
    }
    
}