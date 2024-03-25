using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	public Animator animator;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	float verticalMove = 0f;
	bool isHorizontal = false;
	bool isDown = true;
	bool attacking = false;
	
	// Update is called once per frame
	void Update () {
		if(!animator.GetBool("isDead") && !animator.GetBool("isAttacking"))
		{
			Movement();
			AttackingAnimation();
		}
		else
		{
			horizontalMove = 0f;
			verticalMove = 0f;
		}
		
	}

	void FixedUpdate ()
	{
		// Move our character
		if(isHorizontal)
			controller.Move(horizontalMove * Time.fixedDeltaTime, isHorizontal, attacking);
		else
			controller.Move(verticalMove * Time.fixedDeltaTime, isHorizontal, attacking);
	
	}


	private void Movement()
	{
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		verticalMove = Input.GetAxisRaw("Vertical") * runSpeed;
		
		if(horizontalMove > 0)
		{
			isHorizontal = true;
			animator.SetFloat("Horizontal", Math.Abs(horizontalMove));
			animator.SetFloat("Vertical", 0f);
		}
		else if(horizontalMove < 0)
		{
			isHorizontal = true;
			animator.SetFloat("Horizontal", Math.Abs(horizontalMove));
			animator.SetFloat("Vertical", 0f);
		}
		else if(verticalMove > 0)
		{
			isHorizontal = false;
			isDown = false;
			animator.SetFloat("Vertical", Math.Abs(verticalMove));
			animator.SetFloat("Horizontal", 0f);
		}
		else if(verticalMove < 0)
		{
			isHorizontal = false;
			isDown = true;
			animator.SetFloat("Vertical", Math.Abs(verticalMove));
			animator.SetFloat("Horizontal", 0f);
		}
		else
		{
			animator.SetFloat("Vertical", 0f);
			animator.SetFloat("Horizontal", 0f);
		}
		MovementAnimation();
		
	}
	private void AttackingAnimation()
	{
		if(Input.GetButtonDown("Fire1"))
		{
			attacking = true;
			animator.SetBool("isAttacking", true);
		}

	}
	private void MovementAnimation()
	{
		if(isHorizontal)
			animator.SetBool("isHorizontal", true);
		else
			animator.SetBool("isHorizontal", false);

		if(isDown)
			animator.SetBool("isDown", true);
		else
			animator.SetBool("isDown", false);
	}
	public void StopAttacking()
	{
		attacking = false;
		animator.SetBool("isAttacking", false);
	}
}
