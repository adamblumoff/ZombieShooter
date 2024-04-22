using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [SerializeField] private float _currentHealth;

    [SerializeField] private float _maximumHealth;

    public UnityEvent OnHealthChanged;

    public UnityEvent OnDied;

    private CharacterController2D character;
    private PlayerMovement playerMovement;
    public int damage;
    public int roundNum;


    
    void Start()
    {
        character = GetComponent<CharacterController2D>();
        playerMovement = GetComponent<PlayerMovement>();
        damage = 50;
        //_maximumHealth = character.health;
        roundNum = 0;
    }
    void Update()
    {
        IncreaseRoundNum();
        //_currentHealth = character.health;
    }
    public float RemainingHealthPercentage
    {
        get
        {
            return _currentHealth / _maximumHealth;
        }
    }
    public int GetDamage()
    {
        return damage;
    }
    public void IncreaseDamage()
    {
        damage*=2;
    }

    public void takeDamage(float damageAmount)
    {
        if(_currentHealth == 0)
        {
            return;
        }
        _currentHealth -= damageAmount;

        OnHealthChanged.Invoke();

        if(_currentHealth < 0)
        {
            _currentHealth = 0;
        }

        if(_currentHealth == 0)
        {
            OnDied.Invoke(); 
        }
         

    }
    public int GetRoundNum()
    {
        return roundNum;
    }
    public void IncreaseRoundNum()
    {

    }


}