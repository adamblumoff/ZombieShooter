using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TestCharacterController : MonoBehaviour
{
    [SerializeField] private LayerMask WhatIsGround;   // Mask determining what is ground to the character
    [SerializeField] private Transform GroundCheck;    // Position marking where to check if the player is grounded
    public float health = 100f;                        // Player's health
    public int damage = 50;                            // Damage player can take

    const float k_GroundedRadius = .2f;                // Radius of the overlap circle to determine if grounded
    private bool Grounded;                             // Whether or not the player is grounded
    private Rigidbody2D Rigidbody2D;                   // Player's Rigidbody component
    private bool FacingRight = true;                   // For determining which way the player is currently facing
    private Vector3 stop = Vector3.zero;               // Vector for stopping movement
    private Animator playerAnimator;                   // Animator component of the player
    private bool dead = false;                         // Whether the player is dead
    private bool isAttacking;                          // If the player is attacking
    public bool isHit = false;                         // If the player is hit

    [Header("Events")]
    [Space]
    public UnityEvent OnLandEvent;                     // Event triggered when player lands
    public UnityEvent<float> OnHealthChanged;          // Event triggered on health change

    void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();     // Get the Rigidbody2D component
        playerAnimator = GetComponent<Animator>();     // Get the Animator component
        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f); // Set initial color of sprite

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnHealthChanged == null)
            OnHealthChanged = new UnityEvent<float>();
    }

    void Update()
    {
        GroundCheckPhysics();                          // Check if player is grounded
        CheckDeath();                                  // Check if player needs to play death animation
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;                        // Reduce health by damage amount
        StartCoroutine(TakesHit());                    // Start hit reaction
        OnHealthChanged.Invoke(health / 100.0f);       // Update health bar
        if (health <= 0)
        {
            dead = true;                               // Set dead to true if health is 0 or less
        }
    }

    public void RestoreHealth(int healAmount)
    {
        health = Mathf.Min(health + healAmount, 100f); // Restore health and cap at 100
        OnHealthChanged.Invoke(health / 100.0f);       // Update health bar
    }

    private void GroundCheckPhysics()
    {
        bool wasGrounded = Grounded;
        Grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, k_GroundedRadius, WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }

    private void CheckDeath()
    {
        if (dead)
        {
            DieAnimation();                             // Play death animation
            dead = false;                               // Reset dead flag
        }
    }

    private void DieAnimation()
    {
        Rigidbody2D.velocity = stop;                    // Stop all movement
        this.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f, 0.5f); // Change color to red
        playerAnimator.SetBool("isDead", true);         // Trigger death animation
    }

    private IEnumerator TakesHit()
    {
        if (!dead)
        {
            isHit = true;
            this.GetComponent<BoxCollider2D>().enabled = false;
            for (int i = 0; i < 5; i++)
            {
                this.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f, 0.5f);
                yield return new WaitForSeconds(.2f);
                this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
                yield return new WaitForSeconds(.2f);
            }
            isHit = false;
            this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            this.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}