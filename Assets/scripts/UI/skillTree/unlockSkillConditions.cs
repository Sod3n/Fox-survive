using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class unlockSkillConditions : MonoBehaviour
{
    public skillAddFPToUnlock skillAddFPToUnlock;
    private int _currentFP = 0;
    public int currentFP
    {
        get
        {
            return _currentFP;
        }
        set
        {
            _currentFP = value;
            skillAddFPToUnlock.Needs.GetComponent<TextMeshProUGUI>().text = currentFP + " FP / " + unlockFP[skillAddFPToUnlock.unlockCursor] + " FP";
        }
    }
    public int[] unlockFP;
    public int[] unlockEventId;
    public string[] Descriptions;
    public int unlockSkillLevel = 0;
    public Sprite[] LevelFrames;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void upUnlockLevel()
    {
        unlockSkillLevel++;
        if(LevelFrames.Length > unlockSkillLevel)
        {
            transform.Find("Frame").GetComponent<Image>().sprite = LevelFrames[unlockSkillLevel-1];
            if(Descriptions.Length > unlockSkillLevel)
               transform.Find("Description").GetComponent<TextMeshProUGUI>().text = Descriptions[unlockSkillLevel - 1] + "\n\t\t\t\t\t\t↓\n" + Descriptions[unlockSkillLevel];
        }
    }
}
