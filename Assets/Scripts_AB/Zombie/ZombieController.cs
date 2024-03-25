using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ZombieController : MonoBehaviour
{
	public float health;
	public float speed;
	public int damage = 100;

	private Rigidbody2D rb;
	private Animator enemyAnimator;
	private Transform currentPoint;
	private bool dead = false;
	private Vector2 stop = new Vector2(0,0);
	private bool right = true;
	
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		enemyAnimator = GetComponent<Animator>();
	}
	void Update()
	{
		isDead();
		
	}
	private void flip()
	{
		transform.Rotate(0f, 180f, 0f);	
	}

	public void TakeDamage (int damage)
	{
		health -= damage;

		if (health <= 0)
		{
			dead = true;
		}
	}

	public void isDead()
	{
		if(dead)
		{
			DieAnimation();
			dead = false;
		}
	}
	private void DieAnimation()
	{
		rb.velocity = stop;
		enemyAnimator.SetBool("isDead", true);
	}
	void Die()
	{
		Destroy(gameObject);
	}
	/* void OnTriggerEnter2D (Collider2D hitInfo)
	{
		if(hitInfo.gameObject.CompareTag("Player"))
		{
			CharacterController2D player = hitInfo.GetComponent<CharacterController2D>();
			player.TakeDamage(damage);
		}
		

	} */

}
