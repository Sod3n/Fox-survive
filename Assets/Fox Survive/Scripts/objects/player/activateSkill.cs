using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateSkill : MonoBehaviour
{
    public selectedSkills selectedSkills;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Skill1"))
        {
            Debug.Log(selectedSkills.selectedSkillsId[0]);
            selectedSkills.activeSkill(selectedSkills.selectedSkillsId[0]);
        }
        if (Input.GetButtonDown("Skill2"))
        {
            selectedSkills.activeSkill(selectedSkills.selectedSkillsId[1]);
        }
        if (Input.GetButtonDown("Skill3"))
        {
            selectedSkills.activeSkill(selectedSkills.selectedSkillsId[2]);
        }
        if (Input.GetButtonDown("Skill4"))
        {
            selectedSkills.activeSkill(selectedSkills.selectedSkillsId[3]);
        }
        if (Input.GetButtonDown("Skill5"))
        {
            selectedSkills.activeSkill(selectedSkills.selectedSkillsId[4]);
        }
    }
}
