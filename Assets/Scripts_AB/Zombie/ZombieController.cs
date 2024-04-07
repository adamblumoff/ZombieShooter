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
    private Transform playerTransform; // Reference to the player's transform
    private bool dead = false;
    private Vector2 stop = new Vector2(0, 0);
    private bool right = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
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
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);

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

    public void isDead()
    {
        if (dead)
        {
            Die();
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
}
