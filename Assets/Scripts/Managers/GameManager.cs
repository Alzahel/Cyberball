using System.Collections;
using System.Collections.Generic;
using Cyberball.Network;
using Cyberball.Spawn;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cyberball.Managers
{
    class GameManager : NetworkBehaviour
    {
        public static GameManager instance;

        [SerializeField] private List<NetworkGamePlayer> players = new List<NetworkGamePlayer>();

        public List<NetworkGamePlayer> Players { get => players; set => players = value; }
        public bool IsRoundOver { get => isRoundOver; set => isRoundOver = value; }

        #region Unity Callbacks

        private void Awake()
        {
            if (instance == null) instance = this;
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
                else if (isRoundOver || currentRound == 0) StartNewRound();
                 
        }

        #endregion

        #region Start Game & Rounds management

        private bool allPlayersLoaded = false;
        private bool isRoundOver = false;
        private int currentRound = 0;
  
        [Server]
        private bool AllPlayersLoadedCheck()
        {
            Debug.Log("waiting for players to load");

            allPlayersLoaded = true;
            foreach (NetworkGamePlayer player in players)
            {
                if (!player.HasLoaded) allPlayersLoaded = false;
            }

            return allPlayersLoaded;
        }

        [Server]
        private void StartNewRound()
        {
            Debug.Log("New round setup");

            isRoundOver = false;
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
            if (isRoundOver) return;

            if (teamID == 1) team1Score++;
            else team2Score++;

            isRoundOver = true;
            
            //StartCoroutine(ResetAfterGoal());


            Debug.Log("team 1 : "+ team1Score + "team 2 : " + team2Score );
        }

        #endregion
    }
}
