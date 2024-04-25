using UnityEngine;
using UnityEngine.UI; 
public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    private Image _healthBarForegroundImage; 
    private TestCharacterController player;  

    private void Start()
    {
        
        player = FindObjectOfType<TestCharacterController>();  
        if (player != null)
        {
            
            player.OnHealthChanged.AddListener(UpdateHealthBar);
        }
    }

    public void UpdateHealthBar(float healthPercentage)
    {
        _healthBarForegroundImage.fillAmount = healthPercentage;  
    }
}