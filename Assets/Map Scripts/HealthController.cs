using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthCOntroller : MonoBehaviour
{
    [SerializeField]
    private float _currentHealth;

    [SerializeField]
    private float _maximumHealth;

    public float RemainingHelathPercentage
    {
        get
        {
            return _currentHealth / _maximumHealth;
        }
    }

    public UnityEvent OnHealthChanged;

    public UnityEvent OnDied;

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

}
