using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Procedurally spawns and recycles ground tiles (chunks) ahead of the player
/// for an endless-runner style environment.
/// </summary>
public class GroundSpawner : MonoBehaviour
{
    public GameObject groundPrefab;   // prefab of one ground tile
    public int initialTiles = 5;      // number of tiles to start with
    public float tileLength = 50f;    // Z-length of one tile
    public Transform player;          // player to follow

    public float safeZone = 15f;      // how far behind before a tile is destroyed

    private List<GameObject> activeTiles = new List<GameObject>();
    private float nextSpawnZ = 0f;

    void Start()
    {
        // Pre-spawn initial ground tiles
        for (int i = 0; i < initialTiles; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        if (!player) return;

        // Spawn next tile when player approaches end
        if (player.position.z - safeZone > (nextSpawnZ - initialTiles * tileLength))
        {
            SpawnTile();
            DeleteBehindTile();
        }
    }

    void SpawnTile()
    {
        Vector3 spawnPos = new Vector3(0f, -1f, nextSpawnZ);
        GameObject tile = Instantiate(groundPrefab, spawnPos, Quaternion.identity);
        activeTiles.Add(tile);
        nextSpawnZ += tileLength;
    }

    void DeleteBehindTile()
    {
        if (activeTiles.Count == 0) return;

        GameObject oldest = activeTiles[0];
        if (player.position.z - oldest.transform.position.z > safeZone + tileLength)
        {
            activeTiles.RemoveAt(0);
            Destroy(oldest);
        }
    }
}
