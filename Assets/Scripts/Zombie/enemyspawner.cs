using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class enemyspawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] zombiePrefabs;
    public float zombieInterval = 5f;
    [SerializeField]
    private Transform[] spawners;
    private int numZombies;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(zombieInterval, zombiePrefabs));
    }

    // Update is called once per frame


    

    private IEnumerator spawnEnemy(float interval, GameObject[] enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(GetRandomPrefab(enemy), GetRandomSpawner(spawners).position, Quaternion.identity);
        numZombies++;
        StopSpawning(numZombies, 15);
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
