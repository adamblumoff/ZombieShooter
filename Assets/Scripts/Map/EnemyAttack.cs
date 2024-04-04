using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private float _damageAmount;

    //private void OnCollisionStay2D(Collision collision)
    //{
        //if (collision.gameObject.GetComponent<PlayerMovement>()) //get player movement component from collison object
        //{
          //  var healthController = collision.gameObject.GetComponent<HealthCOntroller>();
          //  healthController.takeDamage(_damageAmount);
        //}
    //}
}
