using System.Collections.Generic;
using UnityEngine;

public class PlatformLooper : MonoBehaviour
{
    public GameObject[] platformPrefabs;       
    public int initialPlatformCount = 5;      
    public float platformLength = 10f;         
    public Transform player;                   
    public float spawnDistance = 30f;          

    private Queue<GameObject> activePlatforms = new Queue<GameObject>();
    private Vector3 nextSpawnPosition = Vector3.zero;

    private void Start()
    {
       
        for (int i = 0; i < initialPlatformCount; i++)
        {
            SpawnPlatform();
        }
    }

    private void Update()
    {
        if (player.position.z + spawnDistance > nextSpawnPosition.z)
        {
            SpawnPlatform();
            RemoveOldPlatform();
        }
    }

    void SpawnPlatform()
    {
        
        int index = Random.Range(0, platformPrefabs.Length);
        GameObject platform = Instantiate(platformPrefabs[index], nextSpawnPosition, Quaternion.identity);
        activePlatforms.Enqueue(platform);

       
        nextSpawnPosition.z += platformLength;
    }

    void RemoveOldPlatform()
    {
        if (activePlatforms.Count > initialPlatformCount)
        {
            GameObject oldPlatform = activePlatforms.Dequeue();
            Destroy(oldPlatform);
        }
    }
}
