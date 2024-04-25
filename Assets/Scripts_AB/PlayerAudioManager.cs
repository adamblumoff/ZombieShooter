using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerMovement player;  
    private Animator animator;
    public AudioSource attack;
    public AudioSource player_walk;
    public AudioSource player_hit;
    public AudioSource player_die;  
    void Start()
    {
        animator = player.animator; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnAttackSound()
    {
        if(player.attacking)
        {
            Debug.Log("attack");
            attack.Play();
        }
    }
    public void OnWalkSound()
    {
        if(animator.GetBool("isAttacking"))
        {
            player_walk.Play();
        }
    }
    public void OnHitSound()
    {
        if(animator.GetBool("isHit"))
        {
            player_hit.Play();
        }
    }
    public void OnDieSound()
    {
        if(animator.GetBool("isDead"))
        {
            player_die.Play();
        }
    }
}
