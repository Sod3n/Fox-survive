using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class riseUp : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public menuManager menuManager;
    public GameObject player;
    public GameObject spawn;
    private void Start()
    {
        spawn = GameObject.FindGameObjectWithTag("Respawn");
    }
    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        menuManager.activateFirstLayerMenu(menuManager.riseUpScreen);
        player.SetActive(true);
        player.transform.position = spawn.transform.position;
        player.GetComponent<needs>().harvestupdate();
        pause.Pause = !pause.Pause;
    }
}
