using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swoosh_Controller : MonoBehaviour
{
	public int damage = 50;
	public Rigidbody2D rb;
	private float timer;
	private GameController gameController;

	// Use this for initialization
	void Start()
	{
		//gameController = GameObject.Find("GameController").GetComponent<GameController>();
		//damage = gameController.GetDamage();
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
			ZombieController zombie = hitInfo.GetComponent<ZombieController>();
			zombie.TakeDamage(damage);
			Destroy(gameObject);
		}
    }
	public void IncreaseDamage()
	{	
		damage*=2;
		//StartCoroutine(IncreaseDamageCoroutine());
	}
	private IEnumerator IncreaseDamageCoroutine()
	{
		damage*=2;
		Debug.Log("test");
		yield return new WaitForSeconds(5f);
		Debug.Log("working");
		damage = 50;
	}
}
