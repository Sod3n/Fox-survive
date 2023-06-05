using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWVar : MonoBehaviour
{
    public GameObject Ground_01;
    public GameObject Ground_02;
    public GameObject Ground_03;
    public GameObject Tree_01;
    public int minNumberOfTree_01;
    public int maxNumberOfTree_01;
    public float chanceSpawnTree_01;
    public GameObject Tree_02;
    public int minNumberOfTree_02;
    public int maxNumberOfTree_02;
    public float chanceSpawnTree_02;
    public GameObject Rock_01;
    public int minNumberOfRock_01;
    public int maxNumberOfRock_01;
    public float chanceSpawnRock_01;
    public GameObject Rock_02;
    public int minNumberOfRock_02;
    public int maxNumberOfRock_02;
    public float chanceSpawnRock_02;
    public GameObject Rock_03;
    public int minNumberOfRock_03;
    public int maxNumberOfRock_03;
    public float chanceSpawnRock_03;
    public GameObject Rock_04;
    public int minNumberOfRock_04;
    public int maxNumberOfRock_04;
    public float chanceSpawnRock_04;
    //HGR
    public GameObject Mushroom_01;
    public int minNumberOfMushroom_01;
    public int maxNumberOfMushroom_01;
    public float chanceSpawnMushroom_01;
    //HP
    public GameObject Mushroom_02;
    public int minNumberOfMushroom_02;
    public int maxNumberOfMushroom_02;
    public float chanceSpawnMushroom_02;
    public GameObject Bush_01;
    public int minNumberOfBush_01;
    public int maxNumberOfBush_01;
    public float chanceSpawnBush_01;
    public GameObject Bush_02;
    public int minNumberOfBush_02;
    public int maxNumberOfBush_02;
    public float chanceSpawnBush_02;
    public GameObject Grass_01;
    public int minNumberOfGrass_01;
    public int maxNumberOfGrass_01;
    public float chanceSpawnGrass_01;
    public GameObject Grass_02;
    public int minNumberOfGrass_02;
    public int maxNumberOfGrass_02;
    public float chanceSpawnGrass_02;
    public GameObject Stump_01;
    public int minNumberOfStump_01;
    public int maxNumberOfStump_01;
    public float chanceSpawnStump_01;
    public GameObject Branch_01;
    public int minNumberOfBranch_01;
    public int maxNumberOfBranch_01;
    public float chanceSpawnBranch_01;
    public GameObject Wolf;
    public int minNumberOfWolf;
    public int maxNumberOfWolf;
    public float chanceSpawnWolf;

    public List<GameObject> objStorages;
    public GameObject player;
    public Vector3 playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerTransform = player.transform.position;
    }
}
