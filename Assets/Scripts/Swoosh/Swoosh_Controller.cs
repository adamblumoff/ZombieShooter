using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Down_Swoosh : MonoBehaviour
{
	public int damage = 100;
	public Rigidbody2D rb;
	public AudioClip rockClip;
	private float timer;
	private GameObject swoosh;


    private void Start()
    {
        swoosh = GetComponent<GameObject>();
    }
    void Update()
	{
		 timer += Time.deltaTime;
		if(timer>1.5f)
		{
			Destroy(gameObject);
		} 
	}
	void OnTriggerEnter2D (Collider2D hitInfo) //does damage to zombies and knocks them back, destroys on background
	{
		if(hitInfo.gameObject.CompareTag("Zombie"))
		{
            Knockback knockback = hitInfo.GetComponent<Knockback>();
            GeneralZombieController zombie = hitInfo.GetComponent<GeneralZombieController>();
			zombie.TakeDamage(damage);
			knockback.HitBackwards(gameObject.transform.position);

            Destroy(gameObject);
		}
		else if(hitInfo.gameObject.CompareTag("Terrain"))
		{	
			SoundManager.PlayRockSound(rockClip);
            Destroy(gameObject);
        }
        else if (hitInfo.gameObject.CompareTag("Border"))
        {
			SoundManager.PlayRockSound(rockClip);
            Destroy(gameObject);
        }

    }
}
