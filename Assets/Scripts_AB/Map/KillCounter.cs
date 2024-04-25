using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KillCounter : MonoBehaviour
{
    public GameObject killCount;  // Add this for round number display
    private static TextMeshProUGUI killCountText;
    private static int kills;


    // Start is called before the first frame update
    void Start()
    {
        kills = 0;
        killCountText = killCount.GetComponent<TextMeshProUGUI>();
        if (killCountText == null)
        {
            Debug.LogError("Text component is not attached to roundNumberGameObject.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void AddKill()
    {
        kills++;
        killCountText.text = kills.ToString();
    }
}
