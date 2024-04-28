using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BarrelSpawner : MonoBehaviour
{
    public Tilemap spawnMap;
    private List<Vector3Int> availableTiles = new List<Vector3Int>();
    public GameObject barrel;

    private List<Vector3> spawnPositions = new List<Vector3>();

    private Boolean barrelPresent = false;

    // Start is called before the first frame update
    void Awake()
    {
        InitializeSpawnPoints();
        for(int i = 0; i < availableTiles.Count; i++)
        {
            Vector3Int spawnLocation = availableTiles[i];
            Vector3 spawnPosition = spawnMap.GetCellCenterWorld(spawnLocation);
            spawnPositions.Add(spawnPosition); //adds available positions to spawn positions

            Instantiate(barrel, spawnPosition, Quaternion.identity);
        }
    }


    void InitializeSpawnPoints() //adds all positions to available tiles
    {
        for (int n = spawnMap.cellBounds.xMin; n < spawnMap.cellBounds.xMax; n++)
        {
            for (int p = spawnMap.cellBounds.yMin; p < spawnMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = new Vector3Int(n, p, (int)spawnMap.transform.position.z);
                if (spawnMap.HasTile(localPlace))
                {
                    availableTiles.Add(localPlace);
                }
            }
        }
    }

    public void RespawnBarrels() //after round ends, respawn barrels at same position
    {
        for(int i = 0; i < availableTiles.Count; i++)
        {
            Collider[] colliders = Physics.OverlapSphere(spawnPositions[i], 1);

            if (colliders != null)
            {
                barrelPresent = false;
                for (int j = 0; j < colliders.Length; j++)
                {
                    Collider collider = colliders[j];
                    if (collider != null && collider.CompareTag("Barrel"))
                    {
                        barrelPresent = true;
                    }
                }
                if(barrelPresent == false)
                {
                    Instantiate(barrel, spawnPositions[i], Quaternion.identity);
                }
            }
        }
    }

    
}
