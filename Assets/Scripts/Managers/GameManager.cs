using System.Collections;
using System.Collections.Generic;
using Cyberball;
using Cyberball.Spawn;
using Mirror;
using Network;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    internal class GameManager : NetworkBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private List<NetworkGamePlayer> players = new List<NetworkGamePlayer>();

        public List<NetworkGamePlayer> Players => players;
        private bool IsRoundOver { get; set; }

        #region Constantes
        
        public const string PlayerTag = "Player";
        public const string PlayerHeadTag = "PlayerHead";

        #endregion
        
        #region Unity Callbacks

        [Server]
        private void Awake()
        {
            if (Instance == null) Instance = this;
        }
        
        [Server]
        private void Update()
        {
            if (SceneManager.GetActiveScene().name.StartsWith("Scene_Map"))
                if (!allPlayersLoaded) AllPlayersLoadedCheck();
                else if (IsRoundOver || currentRound == 0) StartNewRound();
            
            
            if(Input.GetKeyDown(KeyCode.R)) StartNewRound();
                 
        }

        #endregion

        #region Start Game & Rounds management

        private bool allPlayersLoaded;
        private int currentRound;
  
        [Server]
        private void AllPlayersLoadedCheck()
        {
            Debug.Log("waiting for players to load");

            allPlayersLoaded = true;
            foreach (var player in players)
            {
                if (!player.HasLoaded) allPlayersLoaded = false;
            }
        }

        [Server]
        private void StartNewRound()
        {
            Debug.Log("New round setup");

            IsRoundOver = false;
            currentRound++;

            PlayerSpawnSystem.Instance.SpawnAllPlayers(MatchSettings.TimeBetweenRounds);
            Debug.Log("Round " + currentRound + " started !" );
        }
        
        #endregion

        #region Score management

        [SyncVar] private int team1Score;
        [SyncVar] private int team2Score;

        [Server]
        public void ScoreGoal(int teamID)
        {
            if (IsRoundOver) return;

            if (teamID == 1) team1Score++;
            else team2Score++;

            IsRoundOver = true;
            
            Debug.Log("team 1 : "+ team1Score + "team 2 : " + team2Score );
        }

        #endregion
    }
}
