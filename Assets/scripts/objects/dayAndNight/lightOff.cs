using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightOff : MonoBehaviour
{
    public GameObject dayAndNight;
    private Light light;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dayAndNight.transform.rotation.x > 90)
        {
            light.enabled = false;
        }
        else if (dayAndNight.transform.rotation.x > -90)
        {
            light.enabled = true;
        }
    }
}
