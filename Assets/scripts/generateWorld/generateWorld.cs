using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class generateWorld : MonoBehaviour
{

    public GameObject storage;
    public GameObject manager;
    private GWVar var;

    public List<GameObject> objsStorage;
    public List<GameObject> objects;
    //public List<GameObject> generatedObject;
    public List<GameObject> saveOfGeneratedObject;

    public GameObject player;

    public bool withHole = false;
    public generateWorld genWorld;
    Collider m_Collider;
    Vector3 m_Size;
    public float deepening = 1.3f;
    public int maxGenerationRange = 100;

    public bool genNewObj = false;
    public bool genGround = true;
    public int increaseGen = 1;

    private GameObject go = null;

    private float disableObjRangeminx = 0;
    private float disableObjRangemaxx = 0;
    private float disableObjRangeminz = 0;
    private float disableObjRangemaxz = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_Collider = GetComponent<Collider>();
        m_Size = m_Collider.bounds.size;
        disableObjRangeminx = transform.position.x + m_Size.x / deepening * (maxGenerationRange + 2);
        disableObjRangemaxx = transform.position.x - m_Size.x / deepening * (maxGenerationRange + 2);
        disableObjRangeminz = transform.position.z + m_Size.z / deepening * (maxGenerationRange + 2);
        disableObjRangemaxz = transform.position.z - m_Size.z / deepening * (maxGenerationRange + 2);
        player = GameObject.FindGameObjectWithTag("Player");
        manager = GameObject.FindGameObjectWithTag("Manager");
        var = manager.GetComponent<GWVar>();
        increaseGen = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (genGround)
        {
            m_Collider = GetComponent<Collider>();
            m_Size = m_Collider.bounds.size;
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 2, transform.position.z), transform.TransformDirection(new Vector3(-1, 0, -1)), m_Size.z * 2 / deepening))
            {
                if (!Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 2, transform.position.z), transform.TransformDirection(Vector3.back), m_Size.z / deepening))
                {
                    go = Object.Instantiate(randGround(), new Vector3(transform.position.x, transform.position.y, transform.position.z - m_Size.z / deepening), transform.rotation);
                    go.GetComponent<generateWorld>().genNewObj = true;
                }
                else if (!Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 2, transform.position.z), transform.TransformDirection(Vector3.left), m_Size.z / deepening))
                {
                    go = Object.Instantiate(randGround(), new Vector3(transform.position.x - m_Size.x / deepening, transform.position.y, transform.position.z), transform.rotation);
                    go.GetComponent<generateWorld>().genNewObj = true;
                }
                else
                {
                    genGround = false;
                }
                Debug.DrawRay(transform.position, new Vector3(0, 0, m_Size.z / deepening), Color.red, 1000);
            }
        }
        if (genNewObj)
        {
            StartCoroutine(CoroutinGenObjects());
        }
        //addToStorage();
        //Debug.Log(m_Size.x / deepening * maxGenerationRange);
    }
    void OnCollisionStay(Collision collision)
    {
        if(increaseGen > 0)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                GenerateGround(transform.position);
                increaseGen--;
            }
        }
        

    }

    public void GenerateGround(Vector3 transPos)
    {
        
        RaycastHit[] hits;
        hits = Physics.RaycastAll(new Vector3(transform.position.x, transform.position.y - 2, transform.position.z), transform.TransformDirection(new Vector3(1, 0, 1)), m_Size.z / deepening * (maxGenerationRange + 2));
        for (int i = 1; i < maxGenerationRange + 1; i++)
        {
            if (hits.Length <= i)
            {
                go = Object.Instantiate(randGround(), new Vector3(transPos.x + m_Size.x / deepening * i, transPos.y, transPos.z + m_Size.z / deepening * i), transform.rotation);
                go.GetComponent<generateWorld>().genNewObj = true;
            }
        }
        Debug.Log(hits.Length);
        hits = Physics.RaycastAll(new Vector3(transform.position.x, transform.position.y - 2, transform.position.z), transform.TransformDirection(new Vector3(1, 0, -1)), m_Size.z / deepening * (maxGenerationRange + 2));
        for (int i = 1; i < maxGenerationRange + 1; i++)
        {
            if (hits.Length <= i)
            {
                go = Object.Instantiate(randGround(), new Vector3(transPos.x + m_Size.x / deepening * i, transPos.y, transPos.z - m_Size.z / deepening * i), transform.rotation);
                go.GetComponent<generateWorld>().genNewObj = true;
            }
        }
        hits = Physics.RaycastAll(new Vector3(transform.position.x, transform.position.y - 2, transform.position.z), transform.TransformDirection(new Vector3(-1, 0, 1)), m_Size.z / deepening * (maxGenerationRange + 2));
        for (int i = 1; i < maxGenerationRange + 1; i++)
        {
            if (hits.Length <= i)
            {
                 go = Object.Instantiate(randGround(), new Vector3(transPos.x - m_Size.x / deepening * i, transPos.y, transPos.z + m_Size.z / deepening * i), transform.rotation);
                 go.GetComponent<generateWorld>().genNewObj = true;
            }
        }
        hits = Physics.RaycastAll(new Vector3(transform.position.x, transform.position.y - 2, transform.position.z), transform.TransformDirection(new Vector3(-1, 0, -1)), m_Size.z / deepening * (maxGenerationRange + 2));
        for (int i = 1; i < maxGenerationRange + 1; i++)
        {
            if (hits.Length <= i)
            {
                go = Object.Instantiate(randGround(), new Vector3(transPos.x - m_Size.x / deepening * i, transPos.y, transPos.z - m_Size.z / deepening * i), transform.rotation);
                go.GetComponent<generateWorld>().genNewObj = true;
            }
        }
        
    }
    public GameObject randGround()
    {
        if (Random.Range(0, 7) != 1)
            return var.Ground_01;
        else return var.Ground_02;
    }
    public async void genObjectsAsync(GameObject obj, int minCol, int maxCol, float chanse)
    {
        await Task.Run(() => genObjects(obj, minCol, maxCol, chanse));
    }
    public void genObjects(GameObject obj, int minCol, int maxCol, float chanse)
    {
        for (int i = 0; i < Random.Range(minCol, maxCol); i++)
        {
            if(Random.Range(0, 101) < chanse)
            {
                if (loadInfo.loading)
                {
                    GameObject newObject = Object.Instantiate(obj, new Vector3(Random.Range(transform.position.x - m_Size.x / 2, transform.position.x + m_Size.x / 2), transform.position.y + 5, Random.Range(transform.position.z - m_Size.z / 2, transform.position.z + m_Size.z / 2)), transform.rotation);
                    saveOfGeneratedObject.Add(newObject);
                }
                else
                {
                    bool objSpawned = false;
                    // objsStorage = storage.GetComponentsInChildren<Transform>();
                   // objects = storage.GetComponent<GWVar>().objStorages;
                    /*foreach (GameObject objInStorage in storage.GetComponent<GWVar>().objStorages)
                    {
                        Debug.Log(objInStorage && objInStorage.name.Contains(obj.name));
                        if (objInStorage && objInStorage.name.Contains(obj.name))
                        {
                            objInStorage.gameObject.SetActive(true);
                            objInStorage.transform.position = new Vector3(Random.Range(transform.position.x - m_Size.x / 2, transform.position.x + m_Size.x / 2), transform.position.y + 5, Random.Range(transform.position.z - m_Size.z / 2, transform.position.z + m_Size.z / 2));
                            if (objInStorage.gameObject.GetComponent<Rigidbody>() != null)
                            {
                                //obj.gameObject.gameObject.GetComponent<Rigidbody>().detectCollisions = true;
                                //obj.gameObject.gameObject.GetComponent<Rigidbody>().useGravity = true;
                                //obj.gameObject.gameObject.GetComponent<Rigidbody>().WakeUp();
                            }
                            saveOfGeneratedObject.Add(objInStorage.gameObject);
                            storage.GetComponent<GWVar>().objStorages.Remove(objInStorage);
                            //storage.GetComponent<GWVar>().objStorages.Sort();
                            objSpawned = true;
                            Debug.Log(objInStorage.gameObject.GetComponent<Rigidbody>() != null);
                            break;
                        }
                       // Debug.Log(objInStorage.gameObject.name);
                    }*/
                    
                    if (objSpawned == false)
                    {
                        GameObject newObject = Object.Instantiate(obj, new Vector3(Random.Range(transform.position.x - m_Size.x / 2, transform.position.x + m_Size.x / 2), transform.position.y + 5, Random.Range(transform.position.z - m_Size.z / 2, transform.position.z + m_Size.z / 2)), transform.rotation);
                        saveOfGeneratedObject.Add(newObject);
                    }
                }
            }
        }
    }
    public void addToStorage()
    {
        Debug.Log(disableObjRangeminx);
        if (var.playerTransform.x > disableObjRangeminx || var.playerTransform.x < disableObjRangemaxx)
        {
            foreach(GameObject obj in saveOfGeneratedObject)
            {
                if (obj)
                    obj.SetActive(false);
            }
            Debug.Log(transform.position.x + m_Size.x / deepening * (maxGenerationRange + 2));
        }
        else if (var.playerTransform.z > disableObjRangeminz || var.playerTransform.z < disableObjRangemaxz)
        {
            foreach (GameObject obj in saveOfGeneratedObject)
            {
                if (obj)
                {
                    if (obj)
                        obj.SetActive(false);
                }
            }
            Debug.Log(transform.position.z - m_Size.z / deepening * (maxGenerationRange + 2));
        }
       else
        {
            reGenerateObjects();
        }
    }
    public void reGenerateObjects()
    {
        /*//Debug.Log(saveOfGeneratedObject.Count > 1 && !saveOfGeneratedObject[0].activeInHierarchy);
        if (saveOfGeneratedObject.Count > 1 && reGen)
        {
            reGen = false;
            bool objSpawned = false;
            objsStorage = storage.GetComponent<GWVar>().objStorages;
            for(int i = 0; i < saveOfGeneratedObject.Count; i++)
            {
                GameObject obj = saveOfGeneratedObject[i];
                if(obj != null)
                {
                    for (int q = 0; q < objsStorage.Count; q++)
                    {
                        GameObject objInStorage = objsStorage[i];
                        if (objInStorage != null)
                        {
                            Debug.Log(objInStorage.name.Contains(obj.name));
                            if (objInStorage.name.Contains(obj.name))
                            {
                                objInStorage.transform.position = obj.transform.position;
                                objInStorage.transform.rotation = obj.transform.rotation;
                                saveOfGeneratedObject.Add(objInStorage.gameObject);
                                saveOfGeneratedObject.Remove(obj);
                                objInStorage.gameObject.SetActive(true);
                                objSpawned = true;
                                if (objInStorage.gameObject.gameObject.GetComponent<Rigidbody>() != null)
                                {
                                    objInStorage.gameObject.gameObject.GetComponent<Rigidbody>().useGravity = true;
                                }
                                break;
                            }
                        }
                    }
                }
                if (objSpawned == false)
                {
                    saveOfGeneratedObject.Add(Object.Instantiate(obj));
                }
                
                storage.GetComponent<GWVar>().objStorages.Remove(obj);
            }
        }*/
        foreach (GameObject obj in saveOfGeneratedObject)
        {
                /*//Debug.Log(obj);
                var clone = obj;
                storage.GetComponent<GWVar>().objStorages.Add(clone);
               // Debug.Log(obj);

                //obj.transform.SetParent(storage.transform);
                obj.transform.position = new Vector3(0, 0, 0); 
                obj.SetActive(false);
                if (obj.gameObject.gameObject.GetComponent<Rigidbody>() != null)
                {
                    obj.gameObject.gameObject.GetComponent<Rigidbody>().Sleep();
                }*/
                if(obj)
                    obj.SetActive(true);
        }
    }
    IEnumerator CoroutinGenObjects()
    {
        genNewObj = false;
        yield return new WaitForSeconds(0.1f);
        genObjects(var.Tree_01, var.minNumberOfTree_01, var.maxNumberOfTree_01, var.chanceSpawnTree_01);
        genObjects(var.Tree_02, var.minNumberOfTree_02, var.maxNumberOfTree_02, var.chanceSpawnTree_02);
      //  yield return new WaitForSeconds(0.1f);
        genObjects(var.Rock_01, var.minNumberOfRock_01, var.maxNumberOfRock_01, var.chanceSpawnRock_01);
        genObjects(var.Rock_02, var.minNumberOfRock_02, var.maxNumberOfRock_02, var.chanceSpawnRock_02);
        genObjects(var.Rock_03, var.minNumberOfRock_03, var.maxNumberOfRock_03, var.chanceSpawnRock_03);
        genObjects(var.Rock_04, var.minNumberOfRock_04, var.maxNumberOfRock_04, var.chanceSpawnRock_04);
     //   yield return new WaitForSeconds(0.1f);
        genObjects(var.Mushroom_01, var.minNumberOfMushroom_01, var.maxNumberOfMushroom_01, var.chanceSpawnMushroom_01);
        genObjects(var.Mushroom_02, var.minNumberOfMushroom_02, var.maxNumberOfMushroom_02, var.chanceSpawnMushroom_02);
       // yield return new WaitForSeconds(0.1f);
        genObjects(var.Bush_01, var.minNumberOfBush_01, var.maxNumberOfBush_01, var.chanceSpawnBush_01);
        genObjects(var.Bush_02, var.minNumberOfBush_02, var.maxNumberOfBush_02, var.chanceSpawnBush_02);
     //   yield return new WaitForSeconds(0.1f);
        genObjects(var.Stump_01, var.minNumberOfStump_01, var.maxNumberOfStump_01, var.chanceSpawnStump_01);
        genObjects(var.Branch_01, var.minNumberOfBranch_01, var.maxNumberOfBranch_01, var.chanceSpawnBranch_01);
      //  yield return new WaitForSeconds(0.1f);
        genObjects(var.Wolf, var.minNumberOfWolf, var.maxNumberOfWolf, var.chanceSpawnWolf);
        loadInfo.loadingProgress += 1;
    }
 }
