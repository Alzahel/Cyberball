using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Mirror;
using TMPro;
using UnityEngine;

public class PlayerNetworkCommands : NetworkBehaviour
{
    public static PlayerNetworkCommands Instance;
    private void Start()
    {
        if (Instance == null) Instance = this;
    }

    [Server]
    public void CmdUpdateScores(GameObject tmpScore)
    {
        int score1 = GameManager.Instance.Team1Score;
        int score2 = GameManager.Instance.Team2Score;
        RpcUpdateScores(tmpScore, score1,score2 );
    }
        
    [ClientRpc]
    public void RpcUpdateScores(GameObject tmpScore, int team1Score, int team2Score)
    {
        tmpScore.GetComponent<TextMeshProUGUI>().text = team1Score + "                  " + team2Score;
    }
}
