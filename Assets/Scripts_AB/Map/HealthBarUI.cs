using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] //so can set from the inspector
    private UnityEngine.UI.Image _healthBarForegroundImage;

    public void UpdateHealthBar(GameController healthController)
    {
        _healthBarForegroundImage.fillAmount = healthController.RemainingHealthPercentage;
    }
}
