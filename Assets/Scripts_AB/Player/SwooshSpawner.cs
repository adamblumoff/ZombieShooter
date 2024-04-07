using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwooshSpawner : MonoBehaviour
{
    public Transform UpSpawner;
    public GameObject UpSwooshPrefab;
    public Transform DownSpawner;
    public GameObject DownSwooshPrefab;
    public Transform SideSpawner;
    public GameObject SideSwooshPrefab;
    private Animator animator;
    public float speed = 5f;
    public float invertedBulletRate;
    private int count; 
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Swoosh (Transform spawner, GameObject prefab)
	{
		if(!animator.GetBool("isDead"))
        {
			GameObject swoosh = Instantiate(prefab, spawner.position, spawner.rotation);
            Rigidbody2D rb = swoosh.GetComponent<Rigidbody2D>();
            
            if(animator.GetBool("isHorizontal"))
            {
                rb.AddForce(spawner.right * speed, ForceMode2D.Impulse);
            }
            else
            {
                if(animator.GetBool("isDown"))
                {
                    rb.AddForce(-spawner.up * speed, ForceMode2D.Impulse);
                }
                else
                {
                    rb.AddForce(spawner.up * speed, ForceMode2D.Impulse);
                }
            
            }

        }
        
    }
    public void SwooshAttack()
    {
        if(animator.GetBool("isAttacking"))
        {
            if(animator.GetBool("isHorizontal"))
            {
                Swoosh(SideSpawner, SideSwooshPrefab);
            }
            else
            {
                if(animator.GetBool("isDown"))
                {
                    Swoosh(DownSpawner, DownSwooshPrefab);
                }
                else
                {
                    Swoosh(UpSpawner, UpSwooshPrefab);
                }
            
            }
            
        }
    }
}
