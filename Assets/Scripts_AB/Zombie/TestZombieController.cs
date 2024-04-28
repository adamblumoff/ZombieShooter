using UnityEngine;

public class TestZombieController : MonoBehaviour
{
    public float health;
    public int damage = 100;
    private MovementAndAnimation movementAndAnimation;
    private Transform targetTransform; // Can be set to the player's transform or another target
    private bool dead = false;

    void Start()
    {
        movementAndAnimation = GetComponent<MovementAndAnimation>();
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (!dead)
        {
            movementAndAnimation.Move(targetTransform.position);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CharacterController2D player = collision.gameObject.GetComponent<CharacterController2D>();
            if (player != null)
            {
                player.TakeDamage(damage);
                Debug.Log("Damage taken");
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        movementAndAnimation.TriggerHitAnimation();
        if (health <= 0 && !dead)
        {
            dead = true;
            Die();
        }
    }

    void Die()
    {
        movementAndAnimation.TriggerDeathAnimation();
        NotifyKillCounter();
        EndDeath();
    }

    private void NotifyKillCounter()
    {
        KillCounter killCounter = FindObjectOfType<KillCounter>();
        if (killCounter != null)
        {
            killCounter.AddKill();
        }
    }

    void EndDeath()
    {
        Debug.Log("Zombie destroyed");
        Destroy(gameObject);
    }
}

