using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class generateWorldSV : MonoBehaviour
{

    public float deepening = 1.3f;
    public int maxGenerationRange = 10;

    public GameObject manager;

    private GWVar var;

    private MeshRenderer groundCollider;
    private Vector3 groundSize;

    private Vector3 playerPos;
    private Vector3 lastPlayerPos = new Vector3();

    private GameObject ground;

    private Vector3 groundPos;
    private List<Vector3> generatedGrounds = new List<Vector3>();

    //location generate
    private float chanceOfNewLocation = 100;
    private int currentLocation = 0;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager");
        var = manager.GetComponent<GWVar>();
        groundCollider = var.Ground_01.GetComponent<MeshRenderer>();
        groundSize = groundCollider.bounds.size / deepening;
        playerPos = var.playerTransform;
        createGroundAroundCell(getGroundCell(new Vector3()));
        loadInfo.loading = false;
    }

    // Update is called once per frame
    void Update()
    {
        checkPlayerPos();
    }
    private void checkPlayerPos()
    {
        playerPos = var.playerTransform;
        if (Mathf.Abs(playerPos.x - lastPlayerPos.x) > groundSize.x)
        {
            createGroundAroundCell(getGroundCell(playerPos));
            lastPlayerPos.x = playerPos.x;
        }
        if (Mathf.Abs(playerPos.z - lastPlayerPos.z) > groundSize.z)
        {
            createGroundAroundCell(getGroundCell(playerPos));
            lastPlayerPos.z = playerPos.z;
        }
    }
    private void createGroundAroundCell(Vector3 cell)
    {
        groundPos = new Vector3(cell.x * groundSize.x, 0, cell.z * groundSize.z);
        if (!generatedGrounds.Contains(groundPos))
        {
            ground = UnityEngine.Object.Instantiate(randGround(), groundPos, new Quaternion());
            generatedGrounds.Add(groundPos);
            GenAllObjects(groundPos);
            //ground.GetComponent<generateWorld>().genNewObj = true;
        }
        for(int i = maxGenerationRange/-2; i < maxGenerationRange/2; i++)
        {
            for(int q = maxGenerationRange / -2; q < maxGenerationRange / 2; q++)
            {
                groundPos = new Vector3((cell.x-i) * groundSize.x, 0, (cell.z-q) * groundSize.z);
                if (!generatedGrounds.Contains(groundPos))
                {
                    ground = UnityEngine.Object.Instantiate(randGround(), groundPos, new Quaternion());
                    generatedGrounds.Add(groundPos);
                    GenAllObjects(groundPos);
                    //ground.GetComponent<generateWorld>().genNewObj = true;
                }
                //Debug.Log(!generatedGrounds.Contains(groundPos));
            }
        }
    }
    private Vector3 getGroundCell(Vector3 pos)
    {
        pos.x = Mathf.RoundToInt(pos.x / groundSize.x);
        pos.z = Mathf.RoundToInt(pos.z / groundSize.z);
        return pos;
    }

    public GameObject randGround()
    {
        if(currentLocation == 1)
        {
            if (Random.Range(0, 7) != 1)
                return var.Ground_01;
            else if (Random.Range(0, 7) != 1)
                return var.Ground_02;
            else return var.Ground_03;
        }
        else
        {
            if (Random.Range(0, 7) != 1)
                return var.Ground_01;
            else if (Random.Range(0, 7) != 1)
                return var.Ground_02;
            else return var.Ground_03;
        }
    }
    public void genObjects(GameObject obj, int minCol, int maxCol, float chanse, Vector3 pos)
    {
        for (int i = 0; i < Random.Range(minCol, maxCol); i++)
        {
            if (Random.Range(0, 101) < chanse)
            {
                GameObject newObject = Object.Instantiate(obj, new Vector3(Random.Range(pos.x - groundSize.x / 2, pos.x + groundSize.x / 2), pos.y + 5, Random.Range(pos.z - groundSize.z / 2, pos.z + groundSize.z / 2)), new Quaternion());
            }
        }
    }
    private void GenAllObjects(Vector3 pos)
    {
        genObjects(var.Tree_01, var.minNumberOfTree_01, var.maxNumberOfTree_01, var.chanceSpawnTree_01, pos);
        genObjects(var.Tree_02, var.minNumberOfTree_02, var.maxNumberOfTree_02, var.chanceSpawnTree_02, pos);
        //  yield return new WaitForSeconds(0.1f);
        genObjects(var.Rock_01, var.minNumberOfRock_01, var.maxNumberOfRock_01, var.chanceSpawnRock_01, pos);
        genObjects(var.Rock_02, var.minNumberOfRock_02, var.maxNumberOfRock_02, var.chanceSpawnRock_02, pos);
        genObjects(var.Rock_03, var.minNumberOfRock_03, var.maxNumberOfRock_03, var.chanceSpawnRock_03, pos);
        genObjects(var.Rock_04, var.minNumberOfRock_04, var.maxNumberOfRock_04, var.chanceSpawnRock_04, pos);
        //   yield return new WaitForSeconds(0.1f);
        genObjects(var.Mushroom_01, var.minNumberOfMushroom_01, var.maxNumberOfMushroom_01, var.chanceSpawnMushroom_01, pos);
        genObjects(var.Mushroom_02, var.minNumberOfMushroom_02, var.maxNumberOfMushroom_02, var.chanceSpawnMushroom_02, pos);
        // yield return new WaitForSeconds(0.1f);
        genObjects(var.Bush_01, var.minNumberOfBush_01, var.maxNumberOfBush_01, var.chanceSpawnBush_01, pos);
        genObjects(var.Bush_02, var.minNumberOfBush_02, var.maxNumberOfBush_02, var.chanceSpawnBush_02, pos);
        //   yield return new WaitForSeconds(0.1f);
        genObjects(var.Stump_01, var.minNumberOfStump_01, var.maxNumberOfStump_01, var.chanceSpawnStump_01, pos);
        genObjects(var.Branch_01, var.minNumberOfBranch_01, var.maxNumberOfBranch_01, var.chanceSpawnBranch_01, pos);
        //  yield return new WaitForSeconds(0.1f);
        genObjects(var.Wolf, var.minNumberOfWolf, var.maxNumberOfWolf, var.chanceSpawnWolf, pos);
    }

    private void chooseLocation()
    {
        if(Random.Range(chanceOfNewLocation, 100) == 100)
        {
            if(Random.Range(0, 1) == 1)
            {
                currentLocation = 1;
            }
            else
            {
                currentLocation = 2;
            }
        }
    }
}
