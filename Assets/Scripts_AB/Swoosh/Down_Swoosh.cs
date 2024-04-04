using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Down_Swoosh : MonoBehaviour
{
    public float speed = 1f;
	public int damage = 40;
	public Rigidbody2D rb;
	private float timer;

	// Use this for initialization
	void Start () 
	{
		rb.velocity = transform.up * speed;
	}

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
