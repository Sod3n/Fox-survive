using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class setSkillFromSelecting : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public selectedSkills selectedSkills;
    public GameObject icon;
    public Sprite nullIcon;
    public skillsInfo activeSkillsInfo;
    private Vector3 localScale = new Vector3();
    public int skillSlotId = 0;
    private bool onPointerUp = false;
    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && onPointerUp)
        {
            Debug.Log("Up");
            for(int i =0; i < selectedSkills.selectedSkillsId.Length; i++)
            {
                if (selectedSkills.selectedSkillsId[i] == selectSkill.selectingSkillId)
                {
                    selectedSkills.selectedSkillsId[i] = 0;
                }
            }
            selectedSkills.selectedSkillsId[skillSlotId] = selectSkill.selectingSkillId;
            foreach (skill q in activeSkillsInfo.skills)
            {
                if (q.unlockEventId[0] == selectSkill.selectingSkillId)
                {
                    icon.GetComponent<Image>().sprite = q.Icon;
                }
            }
            if (selectSkill.selectingSkillId == 0)
            {
                icon.GetComponent<Image>().sprite = nullIcon;
            }
            selectSkill.selectingSkillId = 0;
        }
        else if (selectedSkills.selectedSkillsId[skillSlotId] == 0)
        {
            icon.GetComponent<Image>().sprite = nullIcon;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        onPointerUp = true;
        localScale = transform.localScale;
        transform.localScale = localScale * 1.1f;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        onPointerUp = false;
        transform.localScale = localScale;
    }
}
