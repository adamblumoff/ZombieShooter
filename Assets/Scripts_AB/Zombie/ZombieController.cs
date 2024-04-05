using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;


public class ZombieController : MonoBehaviour
{
    public float health;
    public float speed;
    public int damage = 100;

    private Rigidbody2D rb;
    private Animator animator;
    private Transform playerTransform; // Reference to the player's transform
    private bool dead = false;
    private Vector2 stop = new Vector2(0, 0);
    private bool right = true;

    float absHorizontalMove = 0f;
    float absVerticalMove = 0f;

    bool isHorizontal = false;
    bool isDown = true;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Find the player by tag
    }

    void Update()
    {
        isDead();
        if (!dead)
        {
            FollowPlayer();
            
        }
    }

    private void FollowPlayer()
    {
        // Move towards the player
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        Movement(direction);

        // Flip the zombie to face the player
        if (playerTransform.position.x > transform.position.x && !right || playerTransform.position.x < transform.position.x && right)
        {
            flip();
            right = !right;
        }
    }

    private void flip()
    {
        transform.Rotate(0f, 180f, 0f);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            dead = true;
        }
    }

    private void Movement(Vector2 direction)
    {
        // Use the direction to determine if the zombie is moving down
        isDown = direction.y < 0;
        absHorizontalMove = Mathf.Abs(direction.x);
        absVerticalMove = Mathf.Abs(direction.y);

        if (absHorizontalMove > absVerticalMove)
        {
            isHorizontal = true;
        }
        else
        {
            isHorizontal = false;
        }

        isDown = direction.y < 0 && !isHorizontal;

        MovementAnimation();
    }





    private void MovementAnimation()
    {
       
        animator.SetBool("IsDown", isDown);
        animator.SetBool("IsHorizontal", isHorizontal);
    }
    public void isDead()
    {
        if (dead)
        {
            DieAnimation();
            dead = false;
        }
    }

    private void DieAnimation()
    {
        rb.velocity = stop;
        animator.SetBool("isDead", true);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
