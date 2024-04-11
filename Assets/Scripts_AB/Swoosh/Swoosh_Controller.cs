using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Down_Swoosh : MonoBehaviour
{
	public int damage = 100;
	public Rigidbody2D rb;
	private float timer;

	// Use this for initialization

	void Update()
	{
		 timer += Time.deltaTime;
		if(timer>1.5f)
		{
			Destroy(gameObject);
		} 
	}
	void OnTriggerEnter2D (Collider2D hitInfo)
	{
		if(hitInfo.gameObject.CompareTag("Zombie"))
		{
			ZombieController zombie = hitInfo.GetComponent<ZombieController>();
			zombie.TakeDamage(damage);
			Destroy(gameObject);
		}

    }
}
