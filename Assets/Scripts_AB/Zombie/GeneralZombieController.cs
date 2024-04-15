using UnityEngine;

public class GeneralZombieController : MonoBehaviour
{
    public float health;
    public int damage = 100;
    private MovementAndAnimation movementAndAnimation;
    private Transform targetTransform; // Can be set to player's transform or another target
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
            player.TakeDamage(damage);
            Debug.Log("Damage taken");
        }
    }

    public void TakeDamage(int damage)
    {
        movementAndAnimation.TriggerHitAnimation();
        health -= damage;
        if (health <= 0)
        {
            dead = true;
            Die();
        }
    }

    void Die()
    {
        movementAndAnimation.TriggerDeathAnimation();
        // Additional death logic here
        
    }

    void EndDeath()
    {
        Debug.Log("destroy");
        Destroy(gameObject);
    }
}
