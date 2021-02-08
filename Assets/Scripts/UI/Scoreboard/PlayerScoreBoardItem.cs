using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreBoardItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI usernameText = null;

    [SerializeField] TextMeshProUGUI goalsText = null;
    
    [SerializeField] TextMeshProUGUI killsText = null;

    [SerializeField] TextMeshProUGUI deathsText = null;

    public void Setup(string username, int goals, int kills, int deaths)
    {
        usernameText.text = username;
        goalsText.text = goals.ToString();
        killsText.text = kills.ToString();
        deathsText.text =  deaths.ToString();
    }
}
