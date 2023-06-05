using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class storageInteract : MonoBehaviour
{
    public GameObject playerInfo;
    public GameObject storageInventory;

    private thingsInteract thingsInteract;
    private storage storage;
    private GameObject pickUpedObj;
    // Start is called before the first frame update
    void Start()
    {
        thingsInteract = GetComponent<thingsInteract>();
        
    }
    void Update()
    {
        /*if (Input.GetButtonDown("Eat"))
        {
            if (storageInventory.activeSelf == true)
            {
                pause.Pause = false;
                playerInfo.SetActive(true);
                storageInventory.SetActive(false);
            }
        }*/
    }
    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "storage_1" || collider.tag == "storage_2" || collider.tag == "storage_3")
        {
            if (pickUpedObj != null)
                subInfo.addSubInfo(0, "E - put object to storage");
            else
                subInfo.addSubInfo(0, "E - open storage");
            if (Input.GetButtonDown("Eat"))
            {
                Debug.Log(pickUpedObj != null);
                storage = collider.gameObject.GetComponent<storage>();
                pickUpedObj = thingsInteract.pickUpedObj;
                if (pickUpedObj != null)
                {
                    if (storage.maxCount > storage.curCount)
                    {
                        storage.curCount++;
                        storage.storageObjects.Add(pickUpedObj);
                        thingsInteract.pickUpedObj.SetActive(false);
                        thingsInteract.removeObjFromHands();
                        storageInventory.GetComponent<reloadInventory>().relInvent();
                    }
                }
                else
                {
                    if (storageInventory.activeSelf == false)
                    {
                        pause.Pause = true;
                        Cursor.visible = true;
                        playerInfo.SetActive(false);
                        storageInventory.SetActive(true);
                        storageInventory.GetComponent<reloadInventory>().relInvent();
                    }
                }
            }
        }
        if (storageInventory.activeSelf == true)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                hideStorageInventory();
            }
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "storage_1" || collider.tag == "storage_2" || collider.tag == "storage_3")
        {
            subInfo.removeSubInfo(0);
        }
    }
    public void hideStorageInventory()
    {
        playerInfo.SetActive(true);
        storageInventory.SetActive(false);
        pause.Pause = false;
        Cursor.visible = false;
    }
}
