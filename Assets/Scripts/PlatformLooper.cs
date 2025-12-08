using System.Collections.Generic;
using UnityEngine;

public class PlatformLooper : MonoBehaviour
{
    public GameObject[] platformPrefabs;       // Spawnlanacak platformlar
    public int initialPlatformCount = 5;       // Başlangıçta kaç tane spawn edilecek
    public float platformLength = 10f;         // Her bir prefab'ın uzunluğu
    public Transform player;                   // Oyuncunun transform'u
    public float spawnDistance = 30f;          // Oyuncuya ne kadar yakın olunca yenisi spawnlanmalı

    private Queue<GameObject> activePlatforms = new Queue<GameObject>();
    private Vector3 nextSpawnPosition = Vector3.zero;

    private void Start()
    {
        // İlk platformları sırayla yerleştir
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
        // Rastgele prefab seç
        int index = Random.Range(0, platformPrefabs.Length);
        GameObject platform = Instantiate(platformPrefabs[index], nextSpawnPosition, Quaternion.identity);
        activePlatforms.Enqueue(platform);

        // Sonraki spawn pozisyonunu ileriye kaydır
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
