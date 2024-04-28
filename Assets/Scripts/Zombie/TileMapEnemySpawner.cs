using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TileMapEnemySpawner : MonoBehaviour
{
    public Tilemap spawnMap;                  // Zombie spawning tilemap layer
    public List<GameObject> zombies;          // Zombies to spawn
    public float initialSpawnDelay = 2.0f;    // Spawn delay at round begin
    public float spawnRate = 2f;              // Rate of spawning zombies
    public float spawnAcceleration = 0.9f;    // Decrement of spawn interval per round
    public int zombiesPerRound = 10;          
    public GameObject RoundOverGameObject;    // Round over message
    public GameObject roundNumberGameObject;  // Add this for round number display

    private List<Vector3Int> availableTiles = new List<Vector3Int>(); // List of tiles to spawn
    private int roundNumber = 1;
    private TextMeshProUGUI roundNumberText;

    private int kills;                              // Kill tracker
    private int totalKillsNeeded = 10;              // Kills needed to progress rounds
    public BarrelSpawner barrelSpawnerInstance;     // Barrel Spawner script
    

    private void Update()
    {
        kills = KillCounter.kills;              // Tracking player kills 
        
        if(kills == totalKillsNeeded)
        {
            zombiesPerRound += 5;
            totalKillsNeeded += zombiesPerRound; // Update kills needed
            StartCoroutine(UpdateRound());       // Start next round

        }
    }
    void Start()
    {
       
        InitializeSpawnPoints();
       
        roundNumberText = roundNumberGameObject.GetComponent<TextMeshProUGUI>();
       
        UpdateRoundNumberUI();    // Update the UI to show the first round number
        StartCoroutine(RunRound());
    }

    void InitializeSpawnPoints()   // Adding each tile in ZombieSpawner to available tiles
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

    void UpdateRoundNumberUI() // Updating Round Number Text
    {
        if (roundNumberText != null)
        {
            roundNumberText.text = "Round " + roundNumber; 
        }
          
    }

    IEnumerator RunRound() // Spawning zombies at a random tile in available tiles at designated spawn rate
    {
        yield return new WaitForSeconds(initialSpawnDelay);

        int zombiesSpawned = 0;

        while (zombiesSpawned < zombiesPerRound)
        {
            if (availableTiles.Count > 0)
            {
                Vector3Int spawnLocation = availableTiles[Random.Range(0, availableTiles.Count)];
                Vector3 spawnPosition = spawnMap.GetCellCenterWorld(spawnLocation);
                int zombieIndex = Random.Range(0, zombies.Count);

                Instantiate(zombies[zombieIndex], spawnPosition, Quaternion.identity);
                zombiesSpawned++;
            }

            yield return new WaitForSeconds(spawnRate);
        }

       
    }

    private IEnumerator UpdateRound()
    {
        // Round over logic
        RoundOverGameObject.SetActive(true);
        UpdateRoundNumberUI(); // Update the round number just before showing the round over message
        yield return new WaitForSeconds(3);
        RoundOverGameObject.SetActive(false);
        barrelSpawnerInstance.RespawnBarrels();

        // Prepare for the next round
        spawnRate *= spawnAcceleration;
        roundNumber++;
        Debug.Log("round number incremented");
        UpdateRoundNumberUI(); // Update the round number for the new round

        StartCoroutine(RunRound()); // Start the next round
    }
}
