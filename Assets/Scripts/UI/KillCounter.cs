using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KillCounter : MonoBehaviour
{
    public GameObject killCount;                    // KillCount Canvas
    private static TextMeshProUGUI killCountText;
    public static int kills;


    // Start is called before the first frame update
    void Start()
    {
        kills = 0;
        killCountText = killCount.GetComponent<TextMeshProUGUI>();
    }

    public static void AddKill() // Updating kills and killcount text
    {
        kills++;
        killCountText.text = kills.ToString();
    }
}
