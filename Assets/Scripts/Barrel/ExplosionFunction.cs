using UnityEngine;

public class ExplosionFunction : MonoBehaviour
{

    private int playerDamage = 50;
    private int zombieDamage = 200;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void EndExplosion()
    {
        Destroy(gameObject);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CharacterController2D player = collision.gameObject.GetComponent<CharacterController2D>();
            player.TakeDamage(playerDamage);
            Debug.Log("Damage taken");
        }
        else if (collision.gameObject.CompareTag("Zombie"))
        {
            GeneralZombieController zombie = collision.gameObject.GetComponent<GeneralZombieController>();
            Knockback knockback = collision.gameObject.GetComponent<Knockback>();
            zombie.TakeDamage(zombieDamage);
            knockback.HitBackwards(gameObject.transform.position);
        }
    }
}
