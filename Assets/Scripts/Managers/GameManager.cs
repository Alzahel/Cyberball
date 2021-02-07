using System;
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
        public bool IsRoundOver { get; set; }

        #region Constantes
        
        public const string PlayerTag = "Player";
        public const string PlayerHeadTag = "PlayerHead";

        #endregion
        
        #region Unity Callbacks
        
        private void Awake()
        {
            if (Instance == null) Instance = this;
        }
        
        private void Update()
        {
            if (!isServer) return;
            
            if (SceneManager.GetActiveScene().name.StartsWith("Scene_Map"))
                if (!allPlayersLoaded) AllPlayersLoadedCheck();
                else if (currentRound == 0 && !IsRoundOver) StartCoroutine(StartNewRound());
            
            
        //    if(Input.GetKeyDown(KeyCode.R)) StartCoroutine(StartNewRound());
                 
        }

        #endregion

        #region Start Game & Rounds management

        private bool allPlayersLoaded;
        private int currentRound;

        public float RemainingTimeBeforeNextRound { get; set; } = MatchSettings.TimeBetweenRounds;

        public event EventHandler<NewRoundLaunchedEventArgs> NewRoundLaunched;
        public event EventHandler<EventArgs> NewRoundStarted;

        public class NewRoundLaunchedEventArgs
        {
            public float RemainingTimeBeforeNextRound;
        }
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
        private IEnumerator StartNewRound()
        {
            Debug.Log("New round setup");
            
            IsRoundOver = true;
            //We stop coroutines of the spawn system in order to not respawn players when a round is over
            PlayerSpawnSystem.Instance.StopAllCoroutines();
            
            yield return new WaitForSeconds(2);

            
            RemainingTimeBeforeNextRound = MatchSettings.TimeBetweenRounds;
           
            while (RemainingTimeBeforeNextRound > 0) {
                
                NewRoundLaunched?.Invoke(this, new NewRoundLaunchedEventArgs{RemainingTimeBeforeNextRound = RemainingTimeBeforeNextRound});
                
                yield return new WaitForSeconds (1);
                
                RemainingTimeBeforeNextRound--;
            }
            
            NewRoundStarted?.Invoke(this, EventArgs.Empty);
            
            PlayerSpawnSystem.Instance.SpawnAllPlayers(0);
            
            IsRoundOver = false;
            currentRound++;
            
            Debug.Log("Round " + currentRound + " started !" );
        }

        #endregion

        #region Score management

        [SyncVar] private int team1Score;
        [SyncVar] private int team2Score;
        public int Team1Score
        {
            get => team1Score;
        }

        public int Team2Score
        {
            get => team2Score;
        }
        
        public event EventHandler<EventArgs> GoalScored;
        
        [Server]
        public void ScoreGoal(int teamID)
        {
            if (IsRoundOver) return;

            RpcScoreGoal(teamID);
            GoalScored?.Invoke(this, EventArgs.Empty);

            StartCoroutine(StartNewRound());
            
            Debug.Log("team 1 : "+ team1Score + "team 2 : " + team2Score );
        }

        [ClientRpc]
        private void RpcScoreGoal(int teamID)
        {
            if (teamID == 1) team1Score++;
            else team2Score++;
        }

        #endregion

        #region GlobalEvents

        public event EventHandler<PlayerKilledEventArgs> OnPlayerKilled;
        
        public class PlayerKilledEventArgs
        {
            public string PlayerKilled;
            public string Source;
        }

        public void PlayerKilled(string playerKilled, string source)
        {
            OnPlayerKilled?.Invoke(this, new PlayerKilledEventArgs() { PlayerKilled = playerKilled, Source = source});
        }

        #endregion
    }
}
