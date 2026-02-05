using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;

    [Header("Spacing Settings")]
    [Tooltip("Distance to first obstacle")]
    public float zStart = 0.1f;

    public float zInterval = 0.25f;

    public float zEnd = 2f;

    [Header("Density Control")]
    [Range(0, 1)]
    public float rowSpawnChance = 0.6f; // 60% chance means frequent empty spaces

    [Range(1, 2)]
    public int maxObstaclesPerRow = 1; // Keeping it at 1 makes it very easy to navigate

    public float laneDistance = 2.5f; // Increased slightly to match wider spacing

    void Start()
    {
        // Start spawning from zStart up to zEnd
        for (float z = zStart; z <= zEnd; z += zInterval)
        {
            Debug.Log(z);
            // This 'if' creates the "breathing room" you're looking for
            if (Random.value < rowSpawnChance)
            {
                SpawnObstacleRow(z);
            }
        }
    }

    void SpawnObstacleRow(float z)
    {
        if (obstaclePrefabs.Length == 0) return;

        // Force a maximum of 1 or 2 obstacles so there is ALWAYS a way through
        int obstaclesToSpawn = Random.Range(1, maxObstaclesPerRow + 1);
        List<int> availableLanes = new List<int> { 0, 1, 2 };

        for (int i = 0; i < obstaclesToSpawn; i++)
        {
            int randomIndex = Random.Range(0, availableLanes.Count);
            int selectedLane = availableLanes[randomIndex];
            availableLanes.RemoveAt(randomIndex);

            float x = (selectedLane - 1) * laneDistance;

            // Use transform.TransformPoint so obstacles move with the tile
            Vector3 spawnPos = transform.TransformPoint(new Vector3(x, 0.5f, z));

            GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            Instantiate(prefab, spawnPos, Quaternion.identity, transform);
        }
    }
}