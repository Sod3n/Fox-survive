using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class storageInteract : MonoBehaviour
{
    public GameObject playerInfo;
    public GameObject storageInventory;
    public menuManager menuManager;

    private thingsInteract thingsInteract;
    private GameObject storageObj;
    private GameObject pickUpedObj;
    // Start is called before the first frame update
    void Start()
    {
        thingsInteract = GetComponent<thingsInteract>();
        
    }
    void Update()
    {
        if (Input.GetButtonDown("Eat") && storageObj)
        {
            storage storage = storageObj.GetComponent<storage>();
            pickUpedObj = thingsInteract.pickUpedObj;

            if (pickUpedObj != null)
            {
                if (storage.maxCount <= storage.curCount) return;
                storage.curCount++;
                storage.storageObjects.Add(pickUpedObj);
                thingsInteract.pickUpedObj.SetActive(false);
                thingsInteract.removeObjFromHands();
                storageInventory.GetComponent<reloadInventory>().relInvent();
            }
            else
            {
                menuManager.activateFirstLayerMenu(menuManager.storage);
            }
        }
        
        if (storageInventory.activeInHierarchy && Input.GetButtonDown("Cancel")) menuManager.activateFirstLayerMenu(menuManager.storage);
    }
    private string[] storages =
    {
        "storage_1",
        "storage_2",
        "storage_3"
    };
    void OnTriggerEnter(Collider collider)
    {
        if (!storages.Contains(collider.tag)) return;
        
        if (pickUpedObj != null) subInfo.addSubInfo(0, "E - put object to storage");
        else subInfo.addSubInfo(0, "E - open storage");

        storageObj = collider.gameObject;
    }
    private void OnTriggerExit(Collider collider)
    {
        if (storages.Contains(collider.tag))
        {
            subInfo.removeSubInfo(0);
            storageObj = null;
        }
    }
}
