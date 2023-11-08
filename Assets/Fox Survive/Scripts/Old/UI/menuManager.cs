using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuManager : MonoBehaviour
{
    public List<GameObject> menus = new List<GameObject>();
    public int mainMenu;
    public int skillMenu;
    public int skills;
    public int plInf;
    public int statistic;
    public int riseUpScreen;
    public int hints;
    public int storage;

    public thingsInteract thingsInteract;

    private int _activeMenu = -1;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && (_activeMenu == -1 || _activeMenu == mainMenu))
        {
            activateFirstLayerMenu(mainMenu);
        }
        if (Input.GetButtonDown("Skill Menu") && (_activeMenu == -1 || _activeMenu == skillMenu) || (Input.GetButtonDown("Cancel") && _activeMenu == skillMenu))
        {
            activateFirstLayerMenu(skillMenu);
        }
        if (Input.GetButtonDown("Statistics") && (_activeMenu == -1 || _activeMenu == statistic) || (Input.GetButtonDown("Cancel") && _activeMenu == statistic))
        {
            statistics.updateStatistics();
            activateFirstLayerMenu(statistic);
        }
        if (Input.GetButtonDown("HideHints"))
        {
            activateMenu(hints, true);
        }
    }
    public void activateFirstLayerMenu(int menu)
    {
        activateMenu(menu);
        activateMenu(skills, true);
        activateMenu(plInf, true);
    }
    public void activateMenu(int menu, bool isSubMenu = false)
    {
        menus[menu].SetActive(!menus[menu].activeInHierarchy);
        if (!isSubMenu)
        {
            Cursor.visible = !Cursor.visible;
            pause.Pause = !pause.Pause;
            if (menus[menu].activeInHierarchy) _activeMenu = menu;
            else _activeMenu = -1;
        }
    }
    public void disableMenus()
    {
        foreach(var menu in menus)
        {
            menu.SetActive(false);
        }
    }
}
