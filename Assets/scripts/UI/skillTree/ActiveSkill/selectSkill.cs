using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class selectSkill : MonoBehaviour, IPointerDownHandler, IPointerExitHandler
{
    bool cursorLeave = true;
    bool iconFollow = false;
    public static int selectingSkillId = 0;
    private GameObject icon;
    public GameObject canvas;
    private GameObject iconParent;
    private Vector3 iconPos;
    // Start is called before the first frame update
    void Start()
    {

        icon = GetComponent<passiveSkillCell>().Icon;
        iconParent = gameObject;
        iconPos = icon.GetComponent<RectTransform>().position;
    }
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            icon.GetComponent<RectTransform>().position = iconPos;
            icon.transform.SetParent(iconParent.transform, true);
            cursorLeave = true;
            iconFollow = false;
        }
        if (iconFollow)
        {
            
            icon.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GetComponent<unlockSkillConditions>().unlockSkillLevel > 0)
        {
            selectingSkillId = 0;
            cursorLeave = false;
            StartCoroutine(startSelectingSkill());
        }
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        cursorLeave = true;
    }
    IEnumerator startSelectingSkill()
    {
        yield return new WaitForSeconds(0.4f);
        if (!cursorLeave)
        {
            selectingSkillId = GetComponent<unlockSkillConditions>().unlockEventId[GetComponent<unlockSkillConditions>().unlockSkillLevel-1];
            skillAddFPToUnlock.selectingSkill = true;
            iconPos = icon.GetComponent<RectTransform>().position;
            icon.transform.SetParent(canvas.transform, true);
            iconFollow = true;
        }
    }
}
