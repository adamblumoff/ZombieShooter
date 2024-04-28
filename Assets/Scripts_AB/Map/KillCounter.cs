using UnityEngine;
using UnityEngine.UI;

public class KillCounter : MonoBehaviour
{
    [SerializeField]
    private Text counterText; // Reference to the UI Text component that displays the kill count
    private int kills = 0;    // Initialize the kill count to zero

    void Start()
    {
        UpdateKillDisplay();  // Update the display at the start to show initial kills, if any
    }

    private void UpdateKillDisplay()
    {
        counterText.text = "Kills: " + kills.ToString();
    }

    public void AddKill()
    {
        kills++;              // Increment the kill count by one
        UpdateKillDisplay();  // Update the UI to reflect the new kill count
    }
}
