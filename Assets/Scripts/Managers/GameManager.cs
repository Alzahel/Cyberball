using System.Collections;
using System.Collections.Generic;
using Cyberball.Spawn;
using Mirror;
using Network;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    class GameManager : NetworkBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private List<NetworkGamePlayer> players = new List<NetworkGamePlayer>();

        public List<NetworkGamePlayer> Players => players;
        private bool IsRoundOver { get; set; }

        #region Unity Callbacks

        private void Awake()
        {
            if (Instance == null) Instance = this;
        }

        private void Start()
        {
            DontDestroyOnLoad(this);
        }

        [Server]
        private void Update()
        {
            if (SceneManager.GetActiveScene().name.StartsWith("Scene_Map"))
                if (!allPlayersLoaded) AllPlayersLoadedCheck();
                else if (IsRoundOver || currentRound == 0) StartNewRound();
                 
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

            StartCoroutine(StartRound());
        }

        [Server]
        private IEnumerator StartRound()
        {
            yield return new WaitForSeconds(5f);
            Debug.Log("Round Started");

            PlayerSpawnSystem.instance.SpawnAllPlayers();

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
            
            //StartCoroutine(ResetAfterGoal());


            Debug.Log("team 1 : "+ team1Score + "team 2 : " + team2Score );
        }

        #endregion
    }
}
