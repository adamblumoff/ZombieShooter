using UnityEngine;
using UnityEngine.UI;

public class RoundCounter : MonoBehaviour
{
    public int currentRound = 1;  // Current round number
    public int zombiesKilledThisRound = 0;  // Number of zombies killed in the current round
    public int killsNeededToAdvanceRound = 5;  // Initial number of kills needed to advance to the next round
    public int killIncreaseEachRound = 5;  // Additional zombies needed to be killed each round

    [SerializeField]
    private Text roundText;  // Reference to the UI Text component that displays the round number

    void Start()
    {
        UpdateRoundDisplay();  // Update the display at the start to show the initial round number
    }

    public void ZombieKilled()
    {
        zombiesKilledThisRound++;
        if (zombiesKilledThisRound >= killsNeededToAdvanceRound)
        {
            AdvanceRound();
        }
    }

    private void AdvanceRound()
    {
        currentRound++;
        zombiesKilledThisRound = 0;  // Reset kill counter for the new round
        killsNeededToAdvanceRound += killIncreaseEachRound;  // Increase the threshold for the next round
        UpdateRoundDisplay();
    }

    private void UpdateRoundDisplay()
    {
        if (roundText != null)
        {
            roundText.text = "Round: " + currentRound.ToString();
        }
        else
        {
            Debug.LogWarning("RoundCounter: The roundText component is not assigned.");
        }
    }
}
