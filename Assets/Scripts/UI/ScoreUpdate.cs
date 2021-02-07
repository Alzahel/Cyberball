using System;
using Managers;
using Mirror;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreUpdate : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tmpScore;

        private void Update
            ()
        {
            tmpScore.text = GameManager.Instance.Team1Score + "                  " + GameManager.Instance.Team2Score;
        }
        
    }
}
