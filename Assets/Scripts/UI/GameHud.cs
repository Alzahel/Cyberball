using System;
using System.Collections;
using Managers;
using Mirror;
using Network;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameHud : NetworkBehaviour
    {
        [SerializeField] private TextMeshProUGUI newRoundCountdownText;
        [SerializeField] private GameObject startText;
        [SerializeField] private GameObject goalText;
        
        // Start is called before the first frame update
        void Start()
        {
            newRoundCountdownText.gameObject.SetActive(false);
            startText.SetActive(false);
            goalText.SetActive(false);
            
            GameManager.Instance.NewRoundLaunched += ShowNewRoundCountDown;
            GameManager.Instance.NewRoundStarted += ShowStartText;
            GameManager.Instance.GoalScored += ShowGoalText;
        }

        private void OnDestroy()
        {
            GameManager.Instance.NewRoundLaunched -= ShowNewRoundCountDown;
            GameManager.Instance.NewRoundStarted -= ShowStartText;
            GameManager.Instance.GoalScored -= ShowGoalText;
        }
        
        [ClientRpc]
        private void ShowNewRoundCountDown(object sender, GameManager.NewRoundLaunchedEventArgs e)
        {
            newRoundCountdownText.gameObject.SetActive(true);
            newRoundCountdownText.text = e.RemainingTimeBeforeNextRound.ToString();
        }
        
        [ClientRpc]
        private void ShowStartText(object sender, EventArgs e)
        {
            newRoundCountdownText.gameObject.SetActive(false);
            StartCoroutine(ShowStartTextCoroutine());
        }

        private IEnumerator ShowStartTextCoroutine()
        {
            startText.SetActive(true);
            yield return  new WaitForSeconds(1);
            startText.SetActive(false);
        }
        
        [ClientRpc]
        private void ShowGoalText(object sender, EventArgs e)
        {
            StartCoroutine(ShowGoalTextCoroutine());
        }

        private IEnumerator ShowGoalTextCoroutine()
        {
            goalText.SetActive(true);
            yield return  new WaitForSeconds(2);
            goalText.SetActive(false);
        }
    }
}
