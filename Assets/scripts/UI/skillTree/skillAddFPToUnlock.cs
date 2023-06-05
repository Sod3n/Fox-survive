using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class skillAddFPToUnlock : MonoBehaviour, IPointerUpHandler
{
    public static bool selectingSkill = false;
    public int spendFPByClick = 20;
    public GameObject Lock;
    public GameObject Needs;
    private int unlockCursor = 0;

    public void Start()
    {
        unlockSkillConditions unlockSkillConditions = GetComponent<unlockSkillConditions>();
        Needs.GetComponent<TextMeshProUGUI>().text = unlockSkillConditions.currentFP + " FP / " + unlockSkillConditions.unlockFP[unlockCursor] + " FP";
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!selectingSkill)
        {
            unlockSkillConditions unlockSkillConditions = GetComponent<unlockSkillConditions>();
            if (unlockSkillConditions.currentFP < unlockSkillConditions.unlockFP[unlockCursor])
            {
                if (unlockSkillConditions.currentFP + spendFPByClick < unlockSkillConditions.unlockFP[unlockCursor])
                {
                    if (spendFPByClick < staticVariable.player.GetComponent<needs>().harvest)
                    {
                        unlockSkillConditions.currentFP += spendFPByClick;
                        staticVariable.player.GetComponent<needs>().harvest -= spendFPByClick;
                        Needs.GetComponent<TextMeshProUGUI>().text = unlockSkillConditions.currentFP + " FP / " + unlockSkillConditions.unlockFP[unlockCursor] + " FP";
                    }

                }
                else
                {
                    if (unlockSkillConditions.unlockFP[unlockCursor] - unlockSkillConditions.currentFP < staticVariable.player.GetComponent<needs>().harvest)
                    {
                        staticVariable.player.GetComponent<needs>().harvest -= unlockSkillConditions.unlockFP[unlockCursor] - unlockSkillConditions.currentFP;
                        skillUnlockEvent.startEvent(unlockSkillConditions.unlockEventId[unlockCursor]);
                        Lock.SetActive(false);
                        unlockSkillConditions.upUnlockLevel();
                        if (unlockCursor < unlockSkillConditions.unlockFP.Length - 1)
                        {
                            unlockCursor++;
                            unlockSkillConditions.currentFP = 0;
                            Needs.GetComponent<TextMeshProUGUI>().text = unlockSkillConditions.currentFP + " FP / " + unlockSkillConditions.unlockFP[unlockCursor] + " FP";
                        }
                        else
                        {
                            Needs.SetActive(false);
                            this.enabled = false;
                        }
                    }
                }
            }
        }
        
    }
}
