using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TileMapEnemySpawner : MonoBehaviour
{
    public Tilemap spawnMap;
    public List<GameObject> zombies;
    public float initialSpawnDelay = 2.0f;
    public float spawnRate = 2.5f;
    public float spawnAcceleration = 0.8f;
    public int zombiesPerRound = 10;
    public GameObject RoundOverGameObject;  // Round over message
    public GameObject roundNumberGameObject;  // Add this for round number display

    private List<Vector3Int> availableTiles = new List<Vector3Int>();
    private int roundNumber = 1;
    private TextMeshProUGUI roundNumberText;

    private int kills;
    private int totalKillsNeeded = 10;
    public BarrelSpawner barrelSpawnerInstance;
    

    private void Update()
    {
        kills = KillCounter.kills;
        if(kills == totalKillsNeeded)
        {
            UpdateRound();

        }
    }
    void Start()
    {
       
        InitializeSpawnPoints();
       
        roundNumberText = roundNumberGameObject.GetComponent<TextMeshProUGUI>();
        if (roundNumberText == null)
        {
            Debug.LogError("Text component is not attached to roundNumberGameObject.");
        }
        UpdateRoundNumberUI();  // Update the UI to show the first round number
        RoundOverGameObject.SetActive(false);
        StartCoroutine(RunRound());
    }

    void InitializeSpawnPoints()
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

    void UpdateRoundNumberUI()
    {
        if (roundNumberText != null)
        {
            Debug.Log("New round number: " + roundNumber);
            roundNumberText.text = "Round " + roundNumber;
        }
          
    }

    IEnumerator RunRound()
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
        zombiesPerRound += 10;
        totalKillsNeeded += zombiesPerRound;
        roundNumber++;
        Debug.Log("round number incremented");
        UpdateRoundNumberUI(); // Update the round number for the new round

        StartCoroutine(RunRound()); // Start the next round
    }
}
