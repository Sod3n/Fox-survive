using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class storage : MonoBehaviour
{
    public GameObject storagedObj;
    public GameObject info;
    public int maxCount;
    public int curCount;
    public int lastCount;
    public float storagedObjWidth = 20;
    public float storagedObjHeight = 20;
    public float storagedObjLength = 20;
    private Vector3 storageSize;
    private Vector3 objSize;
    private float curX;
    private float maxX;
    private float stepX;
    private float curY;
    private float maxY;
    private float stepY;
    private float curZ;
    private float maxZ;
    private float stepZ;

    public List<GameObject> storageObjects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer mesh = storagedObj.GetComponent<MeshRenderer>();
        Vector3 size = mesh.bounds.size;
        Vector3 scale = storagedObj.transform.localScale;
        //scale = toSizeOfAll(scale, size);
        storagedObj.transform.localScale = scale;
        storageSize = GetComponent<MeshFilter>().sharedMesh.bounds.size;
        objSize = storagedObj.GetComponent<MeshFilter>().sharedMesh.bounds.size;
        objSize = new Vector3(objSize.x * scale.x, objSize.y * scale.y, objSize.z * scale.z);
        maxX = storageSize.x / 2;
        maxY = storageSize.y / 2;
        maxZ = storageSize.z / 2;
        stepX = objSize.z/2.5f; // меняем координаты местами, так как поворачиваем объект на 90 градусов по х
        stepY = objSize.x/2.5f;
        stepZ = objSize.y/2f;
        curX = -storageSize.x / 2 + stepX / 2;
        curY = -storageSize.y / 2 + stepY / 2;
        curZ = -storageSize.z / 2 + stepZ / 2;
        instantiateObjs();
        changeStorageInfo();
    }

    // Update is called once per frame
    void Update()
    {
        if(lastCount != curCount)
        {
            showObjects();
            changeStorageInfo();
        }
        lastCount = curCount;
    }
    private async void showObjectsAsync()
    {
        await Task.Run(() => showObjects());
    }
    public void showObjects()
    {
        int count = curCount - lastCount;
        if(count > 0)
        {
            for(int i = lastCount; i < curCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        else if(count < 0)
        {
            for(int i = lastCount-1; i > curCount-1; i--)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        
    }
    private void instantiateObjs()
    {
        curX = -storageSize.x / 2 + stepX / 2;
        curY = -storageSize.y / 2 + stepY / 2;
        curZ = -storageSize.z / 2 + stepZ / 2;
        for (int i = 0; i < maxCount; i++)
        {
            GameObject obj = Object.Instantiate(storagedObj);
            obj.name = "storagedObject";
            obj.transform.position = transform.position;
            obj.transform.SetParent(transform);
            obj.transform.localPosition = new Vector3(curX, curY, curZ);
            obj.transform.localEulerAngles = new Vector3(90, 0, 0);
            curX += stepX;
            if (curX > maxX)
            {
                curX = -storageSize.x / 2 + stepX / 2;
                curY += stepY;
                if (curY > maxY)
                {
                    curY = -storageSize.y / 2 + stepY / 2;
                    curZ += stepZ;
                    if (curZ > maxZ)
                    {
                        break;
                    }
                }
            }
            obj.SetActive(false);
        }
    }
    private Vector3 toSizeOfAll(Vector3 scale, Vector3 size)
    {
        float fcof = size.x / scale.x;
        scale.x = fcof;
        fcof = storagedObjLength / 100 / fcof;
        float scof = size.y / scale.y;
        scale.y = fcof;
        scof = storagedObjHeight / 100 / scof;
        float tcof = size.z / scale.z;
        scale.z = fcof;
        tcof = storagedObjWidth / 100 / tcof;
        float[] c = { fcof, scof, tcof };
        float cof = Mathf.Min(c);
        return new Vector3(cof, cof, cof);
    }
    private void changeStorageInfo()
    {
        var text = info.GetComponent<TextMeshPro>().text;
        info.GetComponent<TextMeshPro>().text = text.Substring(0, text.IndexOf(':')) + ": " + curCount + "/" + maxCount;
    }
    public GameObject removeGameObject(GameObject gameObject)
    {
        for(int i = 0; i < storageObjects.Count; i++)
        {
            if(storageObjects[i].tag == gameObject.tag)
            {
                GameObject gm = storageObjects[i];
                storageObjects.RemoveAt(i);
                curCount--;
                return gm;
            }
        }
        return null;
    }
}
