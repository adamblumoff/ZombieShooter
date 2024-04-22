using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieDropUpgrades : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] upgrades;

    public void RandomDrop()
    {
        if(isDropping())
        {
            GameObject upgrade = Instantiate(GetRandomUpgrade(upgrades), transform.position, Quaternion.identity);
        } 
        
    }
    private GameObject GetRandomUpgrade(GameObject[] upgrades)
    {
        return upgrades[Random.Range(0,upgrades.Length)];
    }
    private bool isDropping()
    {
        int randomNum = Random.Range(0,10);
        if(randomNum < 3)
        {
            return true;
        }
        else    
        {
            return false;
        }
    }
}
