using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class CharacterController2D : MonoBehaviour
{

	[SerializeField] private LayerMask WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform GroundCheck;							// A position marking where to check if the player is grounded.
	public float health = 100f;
	public int damage = 50;
	public AudioClip hitClip;
	public AudioClip dieClip;





	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool Grounded;            // Whether or not the player is grounded.
	private Rigidbody2D Rigidbody2D;
	private bool FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 stop = Vector3.zero;
	private Animator playerAnimator;
	private bool dead = false;
	private bool isAttacking;
	public bool isHit = false;
	


	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }


	void Awake()
	{
		Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		playerAnimator = GetComponent<Animator>();
		this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
	}

	void Update()
	{
		bool wasGrounded = Grounded;
		Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, k_GroundedRadius, WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
		
		if(dead){
			DieAnimation();
			dead = false;
		}
	
	}


	public void Move(float move, bool horizontal, bool attacking)
	{
		if(horizontal)
		{
			Rigidbody2D.MovePosition(Rigidbody2D.position + new Vector2(move * 10f, 0f) * Time.fixedDeltaTime);
			if (move > 0 && !FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		
		else
		{
			Rigidbody2D.MovePosition(Rigidbody2D.position + new Vector2(0f, move * 10f) * Time.fixedDeltaTime);
			
		}
		isAttacking = attacking;
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		FacingRight = !FacingRight;
		transform.Rotate(0f,180f,0);
		
	}
	public void TakeDamage (int damage)
	{
		health -= damage;
		StartCoroutine(TakesHit());
		if (health <= 0)
		{
			dead = true;
		}
	}
	private void DieAnimation()
	{
		Rigidbody2D.velocity = stop;
		this.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f, 0.5f);
		SoundManager.PlayDieSound(dieClip);
		playerAnimator.SetBool("isDead", true);
	}
	public void PlayerDie()
	{
		string currentSceneName = SceneManager.GetActiveScene().name;
		SceneManager.LoadScene(currentSceneName);
	}
	public void RestoreHealth()
	{
		this.health = 100f;
	}
	private IEnumerator TakesHit()
	{
		if(!dead)
		{
			isHit = true;
			SoundManager.PlayHitSound(hitClip);
			this.GetComponent<BoxCollider2D>().enabled = false;
			for(int i = 0; i< 5; i++)
			{
				this.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f, 0.5f);
				yield return new WaitForSeconds(.2f);
				this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
				yield return new WaitForSeconds(.2f);
			}
			isHit = false;
			this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
			this.GetComponent<BoxCollider2D>().enabled = true;
		}

	}

}