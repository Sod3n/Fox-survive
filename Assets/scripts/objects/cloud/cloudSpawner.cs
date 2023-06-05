using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudSpawner : MonoBehaviour
{
    public float spawnRange = 100;
    public int maxCountOfClouds = 20;
    public float minDelayBeforeSpawn = 2;
    public float maxDelayBeforeSpawn = 20;
    public float rangeToCloudDespawn = 100;
    public GameObject[] cloudPrefabs;
    public GameObject player;

    private List<GameObject> clouds = new List<GameObject>();
    private int cursor = 0;
    void Start()
    {
        GameObject go;
        for (int i = 0; i <  maxCountOfClouds; i++)
        {
            go = Instantiate(cloudPrefabs[Random.Range(0, cloudPrefabs.Length)], new Vector3(0, -1000, 0), new Quaternion());
            //go.transform.parent = transform;
            clouds.Add(go);
        }
        StartCoroutine(spawnCloud());
    }
    private void FixedUpdate()
    {
        transform.position = new Vector3(player.transform.position.x,24,player.transform.position.z + rangeToCloudDespawn);
    }
    IEnumerator spawnCloud()
    {
        yield return new WaitForSeconds(Random.Range(minDelayBeforeSpawn, maxDelayBeforeSpawn));
        var heading = clouds[cursor].transform.position - player.transform.position;
        if (heading.sqrMagnitude > rangeToCloudDespawn * rangeToCloudDespawn)
        {
            clouds[cursor].transform.localPosition = new Vector3(Random.Range(-spawnRange / 2, spawnRange / 2), transform.position.y, transform.position.z);
        }
        cursor++;
        if (cursor == maxCountOfClouds)
            cursor = 0;
        StartCoroutine(spawnCloud());
    }
}
