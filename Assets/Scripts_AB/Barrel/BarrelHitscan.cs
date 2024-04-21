using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel_Hitscan : MonoBehaviour
{
    private Animator animator;
    public bool isHit;
    public GameObject explosionPrefab; // Drag your Explosion Prefab here through the Inspector

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit");
        isHit = true;
        animator.SetBool("IsHit", isHit);
    }

    // This method will be called by an animation event
    public void SpawnExplosion()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject); // Destroys the barrel GameObject
    }
}
