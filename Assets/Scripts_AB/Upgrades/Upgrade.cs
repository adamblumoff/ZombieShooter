using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
public class Upgrade : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent AttackUpgradeEvent;
    public GameObject text;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D collider;
    void Start()
    {
        StartCoroutine(DestroyUpgrade());
        text.SetActive(false);
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>(); 
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision is with the player
        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("BlueUpgrade") )
        {
            // Calls different actions
            PlayerMovement movement = collision.gameObject.GetComponent<PlayerMovement>();
            movement.UpgradeSpeed();
            StartCoroutine(BlueShowText());
            
        }
        else if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("GreenUpgrade") )
        {
            // Calls different actions
            CharacterController2D player = collision.gameObject.GetComponent<CharacterController2D>();
            player.RestoreHealth();
            StartCoroutine(GreenShowText());
        }
        else if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("RedUpgrade") )
        {
            // Calls different actions
            AttackUpgradeEvent.Invoke();
            Debug.Log("test");
            Destroy(gameObject);
        }
    }
    private IEnumerator GreenShowText()
    {
        text.SetActive(true);
        spriteRenderer.enabled = false;
        collider.enabled = false;
        yield return new WaitForSeconds(2f);
        text.SetActive(false);
        Destroy(gameObject);
        
    }
    private IEnumerator BlueShowText()
    {
        text.SetActive(true);
        spriteRenderer.enabled = false;
        collider.enabled = false;
        yield return new WaitForSeconds(5f);
        text.SetActive(false);
        Destroy(gameObject);
        
    }
    IEnumerator DestroyUpgrade()
    {
        yield return new WaitForSeconds(7f);
        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
