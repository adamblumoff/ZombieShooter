using System.Collections;
using UnityEngine;

public class GeneralZombieController : MonoBehaviour
{
    public float health;
    public int damage = 100;
    private MovementAndAnimation movementAndAnimation;
    private ZombieDropUpgrades zombieDropUpgrades;
    private Transform targetTransform; // Can be set to player's transform or another target
    private bool dead = false;
    private int gruntDelay = 3;

    public AudioClip zombieGrunt;
    public AudioClip zombieHurt;

    void Start()
    {
        movementAndAnimation = GetComponent<MovementAndAnimation>();
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        zombieDropUpgrades = GetComponent<ZombieDropUpgrades>();

        StartCoroutine(ZombieGrunt(zombieGrunt));
    }

    void Update()
    {
        if (!dead)
        {
            movementAndAnimation.Move(targetTransform.position);
        }
    }

    void OnCollisionEnter2D(UnityEngine.Collision2D collision)
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
        // Additional death logic here
        
    }

    void EndDeath()
    {
        Debug.Log("destroy");
        zombieDropUpgrades.RandomDrop();
        KillCounter.AddKill();
        Destroy(gameObject);
    }


    IEnumerator ZombieGrunt(AudioClip zombieGruntClip)
    {
        while (true)
        {
            yield return new WaitForSeconds(gruntDelay);
            SoundManager.PlayZombieGrunt(zombieGruntClip);
        }
    }
}
