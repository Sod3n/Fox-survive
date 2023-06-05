using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideBrekedHole : MonoBehaviour
{
    //lazy way
    private void OnTriggerStay(Collider col)
    {
        if(col.tag == "BreakedHole")
        {
            col.gameObject.SetActive(false);
        }
    }
}
