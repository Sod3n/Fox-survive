using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class showDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject Description;
    public GameObject Data;
    // Start is called before the first frame update
    public void OnPointerEnter(PointerEventData eventData) 
    {
        try
        {
            Description.SetActive(true);
            Description.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = Data.GetComponent<TextMeshProUGUI>().text;
        }
        catch(Exception er)
        {

        }
    }
    public void OnPointerExit(PointerEventData eventData) 
    {
        try
        {
            Description.SetActive(false);
        }
        catch (Exception er)
        {

        }
    }
}
