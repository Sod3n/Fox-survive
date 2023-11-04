using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class skillsInfo : MonoBehaviour
{
    public GameObject cell;
    public GameObject canvas;
    public GameObject Description;
    public skill[] skills;
    public List<GameObject> skillsObjects = new List<GameObject>();
    //private List<GameObject> skills = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        generateCells();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void generateCells()
    {
        for(int i = 0; i < skills.Length; i++)
        {
            GameObject c = Instantiate(cell);
            c.transform.SetParent(transform);
            c.transform.localScale = new Vector3(1, 1, 1);
            if (skills[i].Icon)
                c.transform.Find("Icon").GetComponent<Image>().sprite = skills[i].Icon;
            if (skills[i].Name != null)
                c.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = skills[i].Name;
            if (skills[i].Description != null)
            {
                c.GetComponent<unlockSkillConditions>().Descriptions = skills[i].Description;
                c.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = skills[i].Description[0];
            }
            if (skills[i].unlockFP.Length > 0)
                c.GetComponent<unlockSkillConditions>().unlockFP = skills[i].unlockFP;
            if (skills[i].unlockEventId.Length > 0)
                c.GetComponent<unlockSkillConditions>().unlockEventId = skills[i].unlockEventId;
            c.GetComponent<showDescription>().Description = Description;
            if (c.GetComponent<selectSkill>())
                c.GetComponent<selectSkill>().canvas = canvas;
            c.transform.localPosition = new Vector3(20, -c.transform.Find("Icon").GetComponent<RectTransform>().rect.height * i - 5 - 20 * i, 0);
            c.GetComponent<unlockSkillConditions>().skillAddFPToUnlock = c.GetComponent<skillAddFPToUnlock>();
            skillsObjects.Add(c);
        }
        GameObject e = new GameObject();
        e.AddComponent<RectTransform>().sizeDelta = new Vector2(100, 10);
        e.transform.SetParent(transform);
    }
    
}

