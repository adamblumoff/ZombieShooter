using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform DownSwooshSpawner;
	public GameObject DownSwooshPrefab;
     public Transform UpSwooshSpawner;
	public GameObject UpSwooshPrefab;
     public Transform SideSwooshSpawner;
	public GameObject SideSwooshPrefab;
	public Animator animator;

	
	// Update is called once per frame
	/* void Update () {
		if (animator.GetBool("isDown"))
		{
			Shoot(DownSwooshSpawner, DownSwooshPrefab);
		}
        else
        {
            Shoot(DownSwooshSpawner, DownSwooshPrefab);
        }
	} */

	public void Shoot (Transform spawner, GameObject prefab)
	{
		if(!animator.GetBool("isDead"))
			Instantiate(prefab, spawner.position, spawner.rotation);
	}
}
