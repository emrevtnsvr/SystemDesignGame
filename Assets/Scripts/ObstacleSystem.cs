using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleSystem : MonoBehaviour
{

    private List<GameObject> allObstacles = new List<GameObject>();
    private Dictionary<GameObject, Vector3> startPositions = new Dictionary<GameObject, Vector3>();
    private Dictionary<GameObject, Quaternion> startRotations = new Dictionary<GameObject, Quaternion>();

    void Awake()
    {
        CacheObstacles();
    }

    private void CacheObstacles()
    {
        allObstacles.Clear();
        startPositions.Clear();
        startRotations.Clear();

        GameObject[] obstacles = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in obstacles)
        {
            if (obj.CompareTag("Obstacle"))
            {
                allObstacles.Add(obj);
                startPositions[obj] = obj.transform.position;
                startRotations[obj] = obj.transform.rotation;
            }
        }
    }

    public void ResetObstacles()
    {
        foreach (GameObject obstacle in allObstacles)
        {
            if (obstacle != null)
            {
                obstacle.SetActive(true);
                obstacle.transform.position = startPositions[obstacle];
                obstacle.transform.rotation = startRotations[obstacle];
            }
        }


    }
}