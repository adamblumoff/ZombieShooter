using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image _healthBarForegroundImage;
    private CharacterController2D player;

    private void Start()
    {
        // Find the player in the scene and reference it
        player = FindObjectOfType<CharacterController2D>();
        if (player != null)
        {
            // Subscribe to the OnHealthChanged event
            player.OnHealthChanged.AddListener(UpdateHealthBar);
        }
    }

    public void UpdateHealthBar(float healthPercentage)
    {
        _healthBarForegroundImage.fillAmount = healthPercentage;
    }
}