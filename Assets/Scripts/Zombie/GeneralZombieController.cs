using System.Collections;
using UnityEngine;

public class GeneralZombieController : MonoBehaviour
{
    public float health;
    public int damage = 100;
    private MovementAndAnimation movementAndAnimation;  
    private ZombieDropUpgrades zombieDropUpgrades;      // Droppable upgrades    
    private Transform targetTransform;                  // Player's transform 
    private bool dead = false;
    private int gruntDelay = 3;

    public AudioClip zombieGrunt;
    public AudioClip zombieHurt;
    private KillCounter killCounter;

    public GameObject zombie;

    void Start()
    {
        zombie = this.gameObject;
        killCounter = FindObjectOfType<KillCounter>();                          // KillCounter gameobject
        movementAndAnimation = GetComponent<MovementAndAnimation>();
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        zombieDropUpgrades = GetComponent<ZombieDropUpgrades>();

        StartCoroutine(ZombieGrunt(zombieGrunt));                               // ZombieGrunt interval
    }

    void Update()
    {
        if (!dead)
        {
            movementAndAnimation.Move(targetTransform.position);                // PlayerTracking
        }
    }

    void OnCollisionEnter2D(UnityEngine.Collision2D collision) // Checks to see if the zombie collides with player, and deals player damage
    {
        if (collision.gameObject.CompareTag("Player") && dead == false)
        {
            CharacterController2D player = collision.gameObject.GetComponent<CharacterController2D>();
            player.TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage)                      // Zombie Damage function
    {
        movementAndAnimation.TriggerHitAnimation();
        health -= damage;
        SoundManager.PlayZombieHurt(zombieHurt);
        if (health <= 0)
        {
            dead = true;
            Die();
        }
    }

    void Die()
    {
        movementAndAnimation.TriggerDeathAnimation();
    }

    void EndDeath()                     // Kill zombie and drop upgrade
    {
        zombieDropUpgrades.RandomDrop();
        KillCounter.AddKill();
        Destroy(gameObject);
    }


    IEnumerator ZombieGrunt(AudioClip zombieGruntClip) // Zombie grunting coroutine
    {
        while (true)
        {
            yield return new WaitForSeconds(gruntDelay);
            SoundManager.PlayZombieGrunt(zombieGruntClip);
        }
    }
}
