using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// spawn obstacles randomly 
/// it should be attached to ground prefab
/// </summary>
public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;

    public float zStart = -4.5f;

    public float zInterval = 3f;

    public float zEnd = 4.5f;

    public float rowSpawnChance = 0.6f; // 60% chance means frequent empty spaces

    public float laneDistance = 2.5f;

    void Start()
    {
        // Start spawning from zStart up to zEnd
        for (float z = zStart; z <= zEnd; z += zInterval)
        {

            if (Random.value < rowSpawnChance)
            {
                SpawnObstacleRow(z);
            }
        }
    }

    void SpawnObstacleRow(float z)
    {
        if (obstaclePrefabs.Length == 0) return;
        List<int> availableLanes = new List<int> { 0, 1, 2 };
        int randomIndex = Random.Range(0, availableLanes.Count);

        int selectedLane = availableLanes[randomIndex];

        float x = (selectedLane - 1) * laneDistance;


        Vector3 spawnPos = transform.TransformPoint(new Vector3(x, 0.5f, z));

        GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        Instantiate(prefab, spawnPos, Quaternion.identity, transform);

    }
}