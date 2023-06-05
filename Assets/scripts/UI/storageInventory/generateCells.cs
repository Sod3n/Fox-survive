using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class generateCells : MonoBehaviour
{
    public GameObject Cell;
    public GameObject Storage;
    public GameObject storageInventory;
    public Sprite standartSprite;
    public float spaceWidth;
    public float spaceHeight;
    private List<GameObject> storageObjects;
    private RectTransform cellRectTransform;
    private RectTransform rectTransform;
    private float width;
    private float cellWidth;
    private float height;
    private float cellHeight;
    private float cellWRaw;
    private float cellHRaw;
    private float emptySpaceWidth;
    private List<GameObject> cells = new List<GameObject>();
    private GameObject icon;
    // Start is called before the first frame update
    void Start()
    {
        cellRectTransform = Cell.GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();
        width = rectTransform.rect.width;
        cellWidth = cellRectTransform.rect.width;
        height = rectTransform.rect.height;
        cellHeight = cellRectTransform.rect.height;
        cellWRaw = width / cellWidth;
        cellHRaw = height / cellHeight;
        while ((cellWRaw * cellWidth) + (cellWRaw+1) * spaceWidth > width)
        {
            cellWRaw--;
        }
        while ((cellHRaw * cellHeight) + (cellHRaw + 1) * spaceHeight > height)
        {
            cellHRaw--;
        }
        Debug.Log(cellHRaw + " " + cellWRaw);
        emptySpaceWidth = width - ((cellWRaw * cellWidth) + (cellWRaw + 1) * spaceWidth);
        storageInventory.SetActive(true);
        createCells();
        storageInventory.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void createCells()
    {
        for (int q = 0; q < cellHRaw; q++)
        {
            for (int i = 0; i < cellWRaw; i++)
            {
                GameObject c = Instantiate<GameObject>(Cell);
                c.transform.SetParent(transform);
                c.GetComponent<RectTransform>().localPosition = new Vector3(cellWidth * i + spaceWidth * (i + 1) + emptySpaceWidth / 2, -(cellHeight * q + spaceHeight * (q + 1)), 0);
                cells.Add(c);
            }
        }
    }
    public void reloadInventory()
    {
        storageObjects = Storage.GetComponent<storage>().storageObjects;
        
        sortedInfo sortedInfo = sortStorageObjects(storageObjects);
        
        int w = 0;

        foreach(GameObject cell in cells)
        {
            if (w < sortedInfo.objects.Count)
            {
                GameObject gameObject = sortedInfo.objects[w];
                if (icon = gameObject.transform.Find("icon").gameObject)
                {
                    icon ic = new icon();
                    ic.sprite = icon.GetComponent<Image>().sprite;
                    ic.gameObject = gameObject;
                    cell.GetComponent<cell>().setIcon(ic);
                }
                else
                {
                    icon icon = new icon();
                    icon.gameObject = gameObject;
                    icon.sprite = standartSprite;
                    cell.GetComponent<cell>().setIcon(icon);
                }
                cell.GetComponent<cell>().setNum(sortedInfo.counts[w]);
                cell.GetComponent<cell>().storedObj = sortedInfo.objects[w];
            }
            else
            {
                cell.GetComponent<cell>().setIcon(null);
                cell.GetComponent<cell>().setNum(0);
                cell.GetComponent<cell>().storedObj = null;
            }

            w++;
        }     
    }
    private sortedInfo sortStorageObjects(List<GameObject> objects)
    {
        List<GameObject> oneObj = new List<GameObject>();
        List<int> countObj = new List<int>();
        for(int i = 0; i < objects.Count; i++)
        {
            bool repeat = false;
            for(int q = 0; q < oneObj.Count; q++)
            {
                if(oneObj[q].tag == objects[i].tag)
                {
                    countObj[q]++;
                    repeat = true;
                    break;
                }
            }
            if (!repeat)
            {
                oneObj.Add(objects[i]);
                countObj.Add(1);
            }
        }
        sortedInfo info = new sortedInfo();
        info.objects = oneObj;
        info.counts = countObj;
        return info;
    }
}
public class sortedInfo{
    public List<GameObject> objects;
    public List<int> counts;
}
