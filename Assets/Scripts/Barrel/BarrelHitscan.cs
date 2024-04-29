using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel_Hitscan : MonoBehaviour
{
    private Animator animator;
    public bool isHit;
    public GameObject explosionPrefab; // Drag your Explosion Prefab here through the Inspector
    public AudioClip explosionClip;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        isHit = true;
        animator.SetBool("IsHit", isHit);
    }

    // This method will be called by an animation event
    public void SpawnExplosion()
    {
        if (explosionPrefab != null)
        {
            SoundManager.PlayBarrelExplosion(explosionClip);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject); // Destroys the barrel GameObject
    }
}
