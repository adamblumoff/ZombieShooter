using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class enemyspawner2 : MonoBehaviour
{
    [SerializeField] private GameObject[] zombiePrefabs;
    public float zombieInterval = 2f;
    [SerializeField] private Transform[] spawners;
    private int numZombies;
    [HideInInspector] public int maxZombies = 15;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(zombieInterval, zombiePrefabs));
    }


    private IEnumerator spawnEnemy(float interval, GameObject[] enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(GetRandomPrefab(enemy), GetRandomSpawner(spawners).position, Quaternion.identity);
        numZombies++;
        StopSpawning(numZombies, maxZombies);  
        StartCoroutine(spawnEnemy(interval, enemy));
    }
    private Transform GetRandomSpawner(Transform[] spawners)
    {
        return spawners[Random.Range(0,spawners.Length)];
    }
    private GameObject GetRandomPrefab(GameObject[] prefabs)
    {
        return prefabs[Random.Range(0,prefabs.Length)];
    }
    private void StopSpawning(int numZombies, int maxZombies)
    {
        if(numZombies == maxZombies)
        {
            StopAllCoroutines();
        }
    }
}
