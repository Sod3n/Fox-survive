using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectedSkills : MonoBehaviour
{
    public int[] selectedSkillsId = new int[5];
    public void activeSkill(int id)
    {
        switch (id)
        {
            case 1:
                Debug.Log("Call firefly");
            break;
        }
    }
}
