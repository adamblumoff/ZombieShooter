using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Down_Swoosh : MonoBehaviour
{
	public int damage = 100;
	public Rigidbody2D rb;
	private float timer;
	private GameObject swoosh;


    // Use this for initialization
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
	void OnTriggerEnter2D (Collider2D hitInfo)
	{
		if(hitInfo.gameObject.CompareTag("Zombie"))
		{
            Knockback knockback = hitInfo.GetComponent<Knockback>();
            GeneralZombieController zombie = hitInfo.GetComponent<GeneralZombieController>();
			zombie.TakeDamage(damage);
			knockback.HitBackwards(gameObject.transform.position);

            Destroy(gameObject);
		}

    }
}
