using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillUnlockEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void startEvent(int id)
    {
        switch (id)
        {
            case 1:
                staticVariable.player.GetComponent<playercontroller>().doubleJump = true;
            break;
            case 2:
                staticVariable.player.GetComponent<playercontroller>().speedBoost = true;
            break;
            case 3:
                staticVariable.player.transform.Find("mouth").GetComponent<thingsInteract>().strength += 1;
            break;
            case 4:
                staticVariable.player.GetComponent<needs>().maxHealth += 25;
            break;
            case 5:
                staticVariable.player.GetComponent<needs>().maxHarvest += 25;
            break;
            case 6:
                staticVariable.player.GetComponent<needs>().speedOfHarvest = 0;
            break;
            case 7:
                
            break;
        }
    }
}
