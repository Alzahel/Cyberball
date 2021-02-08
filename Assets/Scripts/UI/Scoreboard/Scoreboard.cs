using System;
using JetBrains.Annotations;
using Managers;
using Mirror;
using Network;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.Scoreboard
{
    public class Scoreboard : MonoBehaviour
    {
        [SerializeField] GameObject scoreboard = null;
        
        [SerializeField] GameObject playerScoreBoardItem = null;

        [SerializeField] Transform PlayerScoreBoardList = null;

        private void Start()
        {
            scoreboard.gameObject.SetActive(false);
        }

        

        private void FillScoreBoard()
        {
            foreach(NetworkGamePlayer player in GameManager.Instance.Players)
            {
                Debug.Log(player.Username + player.Kills + player.Deaths);
                GameObject itemGO = Instantiate(playerScoreBoardItem, PlayerScoreBoardList);
                PlayerScoreBoardItem item = itemGO.GetComponent<PlayerScoreBoardItem>();
                if(item != null)
                {
                    item.Setup(player.Username, player.Goals, player.Kills, player.Deaths);
                }
            }
        }
        
        private void CleanScoreboard()
        {
            bool firstChild = true;
            foreach (Transform child in PlayerScoreBoardList)
            {
                if(!firstChild) Destroy(child.gameObject);
                firstChild = false;
            }
        }
        
        [UsedImplicitly]
        private void OnScoreboard(InputValue value)
        {
            if (!transform.root.GetComponent<NetworkIdentity>().hasAuthority) return;
        
            var isKeyPushed = Math.Abs(value.Get<float>()) >= 1;

            if (isKeyPushed)
            {
                FillScoreBoard();
                scoreboard.gameObject.SetActive(true);
            }
            else
            {
               CleanScoreboard();
               scoreboard.gameObject.SetActive(false);
                
            }
        }
    }
}
