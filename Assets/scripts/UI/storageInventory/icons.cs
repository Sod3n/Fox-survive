using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class icons : MonoBehaviour
{
    public Sprite standartSprite;

    public Sprite imMushroom_1;
    public GameObject mushroom_1;
    public Image mushroom_2;

    public List<icon> iconsList = new List<icon>();
    // Start is called before the first frame update
    void Start()
    {
        addIcon(imMushroom_1, mushroom_1);
    }
    private void addIcon(Sprite sprite, GameObject gameObject)
    {
        icon icon = new icon();
        icon.sprite = sprite;
        icon.gameObject = gameObject;
        iconsList.Add(icon);
    }
}
public class icon 
{
    public Sprite sprite;
    public GameObject gameObject;
}
