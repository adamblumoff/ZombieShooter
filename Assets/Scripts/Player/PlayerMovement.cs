using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    private SwooshSpawner swooshSpawner;
    private CharacterController2D character;
    public float runSpeed = 40f;
    public AudioClip moveClip;

    float horizontalMove = 0f;
    float verticalMove = 0f;
    bool isHorizontal = false;
    bool isDown = true;
    public bool attacking = false;
    private bool isSoundPlaying = false;


    // Update is called once per frame
    void Start()
    {
        swooshSpawner = FindObjectOfType<SwooshSpawner>();
        character = FindObjectOfType<CharacterController2D>();
    }
    void Update()
    {
        if (!animator.GetBool("isDead") && !animator.GetBool("isAttacking") || swooshSpawner.rapidFire)
        {
            Movement();
            AttackingAnimation();
            MovementSound();
        }
        else
        {
            horizontalMove = 0f;
            verticalMove = 0f;
        }


    }

    void FixedUpdate()
    {
        // Move our character
        if (isHorizontal)
            controller.Move(horizontalMove * Time.fixedDeltaTime, isHorizontal, attacking);
        else
            controller.Move(verticalMove * Time.fixedDeltaTime, isHorizontal, attacking);

    }

    private void MovementSound()
    {
        if((verticalMove > 0 || horizontalMove > 0) && !isSoundPlaying)
            {
                SoundManager.PlayMovementSound(moveClip);
                isSoundPlaying = true;
                StartCoroutine(PlayMoveSound());
            }
    }
    private void Movement()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        verticalMove = Input.GetAxisRaw("Vertical") * runSpeed;
        

        if (horizontalMove > 0)
        {
            isHorizontal = true;
            animator.SetFloat("Horizontal", Math.Abs(horizontalMove));
            animator.SetFloat("Vertical", 0f);
        }
        else if (horizontalMove < 0)
        {   
            isHorizontal = true;
            animator.SetFloat("Horizontal", Math.Abs(horizontalMove));
            animator.SetFloat("Vertical", 0f);
        }
        else if (verticalMove > 0)
        {
            isHorizontal = false;
            isDown = false;
            animator.SetFloat("Vertical", Math.Abs(verticalMove));
            animator.SetFloat("Horizontal", 0f);
        }
        else if (verticalMove < 0)
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
        if (Input.GetButtonDown("Fire1") && !character.isHit || swooshSpawner.rapidFire)
        {
            attacking = true;
            animator.SetBool("isAttacking", true);
            SetRapidFire();
        }

    }
    private void MovementAnimation()
    {
        if (isHorizontal)
            animator.SetBool("isHorizontal", true);
        else
            animator.SetBool("isHorizontal", false);

        if (isDown)
            animator.SetBool("isDown", true);
        else
            animator.SetBool("isDown", false);
    }
    public void StopAttacking()
    {
        attacking = false;
        animator.SetBool("isAttacking", false);
    }

    public void UpgradeSpeed()
    {
        if(this.runSpeed <=40)
            this.runSpeed *= 1.1f;
    }
    public void SetRapidFire()
    {
        if (swooshSpawner.rapidFire)
            StartCoroutine(RapidFire());
    }
    private IEnumerator RapidFire()
    {
        swooshSpawner.rapidFire = true;
        yield return new WaitForSeconds(10f);
        swooshSpawner.rapidFire = false;
    }
    private IEnumerator PlayMoveSound()
    {
        yield return new WaitForSeconds(moveClip.length);
        isSoundPlaying = false;
    }


}
