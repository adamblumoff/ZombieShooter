using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    private SwooshSpawner swooshSpawner;
    private CharacterController2D character;
    public float runSpeed = 40f;

    float horizontalMove = 0f;
    float verticalMove = 0f;
    bool isHorizontal = false;
    bool isDown = true;
    public bool attacking = false;


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
        this.runSpeed *= 1.1f;
    }
    public void SetRapidFire()
    {
        if (swooshSpawner.rapidFire)
            StartCoroutine(RapidFire());
    }
    private IEnumerator RapidFire()
    {
        yield return new WaitForSeconds(10f);
        swooshSpawner.rapidFire = false;
    }

}
