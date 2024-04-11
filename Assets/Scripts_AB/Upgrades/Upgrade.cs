using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyUpgrade());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision is with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Calls different actions
            CharacterController2D player = collision.gameObject.GetComponent<CharacterController2D>();
            PlayerMovement movement = collision.gameObject.GetComponent<PlayerMovement>();
            player.RestoreHealth();
            movement.UpgradeSpeed();
            Destroy(gameObject);
        }
    }
    IEnumerator DestroyUpgrade()
    {
        yield return new WaitForSeconds(7f);
        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
