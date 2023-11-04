using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class cell : MonoBehaviour, IPointerClickHandler
{
    public GameObject player;
    public GameObject storage;
    private GameObject mouth;
    public GameObject storedObj;
    private int col = 0;
    // Start is called before the first frame update
    void Start()
    {
        mouth = player.transform.Find("mouth").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setIcon(icon icon)
    {
        Image image = transform.Find("icon").gameObject.GetComponent<Image>();
        if (icon != null)
        {
            storedObj = icon.gameObject;
            image.sprite = icon.sprite;
            image.color = Color.white;
        }
        else
        {
            image.color = Color.clear;
        }
        
    }
    public void setNum(int num)
    {
        if(num != 0)
        {
            transform.Find("Num").GetComponent<Text>().text = "x" + num;
            col = num;
        }
        else
        {
            transform.Find("Num").GetComponent<Text>().text = "";
            col = 0;
        }
           
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(col + " " + storedObj.name);
        if (col > 0 && storedObj)
        {
            col--;
            GameObject gameObject = storage.GetComponent<storage>().removeGameObject(storedObj);
            mouth.GetComponent<thingsInteract>().pickUp(gameObject);
            mouth.GetComponent<storageInteract>().menuManager.activateFirstLayerMenu(mouth.GetComponent<storageInteract>().menuManager.storage);
        }
    }
}
