using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class statistics : MonoBehaviour
{
    public static int wolfsKilled = 0;
    public static int amanitsEated = 0;
    public static int foodshrumsEated = 0;
    public static int dugHoles = 0;
    public GameObject wolfs;
    public GameObject amanits;
    public GameObject foodshrums;
    public GameObject holes;
    private static statistics Statistics;
    // Start is called before the first frame update
    void Start()
    {
        Statistics = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void updateStatistics()
    {
        Statistics.wolfs.GetComponent<TextMeshProUGUI>().text = wolfsKilled.ToString();
        Statistics.amanits.GetComponent<TextMeshProUGUI>().text = amanitsEated.ToString();
        Statistics.foodshrums.GetComponent<TextMeshProUGUI>().text = foodshrumsEated.ToString();
        Statistics.holes.GetComponent<TextMeshProUGUI>().text = dugHoles.ToString();
    }
}
