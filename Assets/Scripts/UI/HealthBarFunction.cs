using UnityEngine;
using UnityEngine.UI;

public class HealthBarFunction : MonoBehaviour
{
    public static Image fillImage;
    public static float maxHealth = 100f;
    public Image fillImageTemp;

    private void Start()
    {
        fillImage = fillImageTemp;
    }
    public static void UpdateFillAmount(float currentHealth)
    {
        if (fillImage != null)
            fillImage.fillAmount = currentHealth / maxHealth;
    }

}
