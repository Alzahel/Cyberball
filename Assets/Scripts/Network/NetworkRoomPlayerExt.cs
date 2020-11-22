using Mirror;
using UnityEngine;

namespace Network
{
    public class NetworkRoomPlayerExt : NetworkRoomPlayer
    {

        #region Variables
        [SyncVar(hook = nameof(HandleUsernameChanged))] private string username;
        [SyncVar(hook = nameof(HandleTeamChanged))] private int teamID;

        [SerializeField] private bool isLeader;
        #endregion

        #region Properties

        public int TeamID { get => teamID; set => teamID = value; }
        public string Username { get => username; set => username = value; }
        public bool IsLeader
        { 
            get => isLeader; 
            set 
            { 
                isLeader = value;
                UiLobbySetup.Instance.startButton.gameObject.SetActive(value);
            }
        }

        #endregion

        #region SyncVar hooks

        public void HandleTeamChanged(int oldValue, int newValue) 
        { 
            LobbyDisplay.Instance.UpdateDisplay();
            if (newValue == 1)
            {
                NetworkRoomManagerExt.Instance.team1.Add(gameObject);
                if(oldValue ==2) NetworkRoomManagerExt.Instance.team2.Remove(gameObject);
            }
            else
            {
                NetworkRoomManagerExt.Instance.team2.Add(gameObject);
                if(oldValue == 1) NetworkRoomManagerExt.Instance.team1.Remove(gameObject);
            }
        }

        public void HandleUsernameChanged(string oldValue, string newValue) => LobbyDisplay.Instance.UpdateDisplay();
        public override void ReadyStateChanged(bool oldValue, bool newValue) => LobbyDisplay.Instance.UpdateDisplay();
        public override void IndexChanged(int oldValue, int newValue) 
        {
            if (!hasAuthority) return;

            if (newValue == 0) IsLeader = true;
            else IsLeader = false;
        }

        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            UiLobbySetup.Instance.readyButton.onClick.AddListener(SetReadyToBegin);
            UiLobbySetup.Instance.changeTeam1.onClick.AddListener(ChangeTeam1);
            UiLobbySetup.Instance.changeTeam2.onClick.AddListener(ChangeTeam2);
        }

        #endregion

        #region setSyncVars

        private void SetReadyToBegin()
        {
            CmdChangeReadyState(!readyToBegin);
        }

        [Command]
        private void SetUsername(string username)
        {
            this.username = username;
        }

        [Command]
        private void CmdSetTeamID(int teamID)
        {
            this.teamID = teamID;
        }

        #endregion

        #region Team Management

        private void AddToTeamWithLessMembers()
        {
            int team1Members = NetworkRoomManagerExt.Instance.team1.Count;
            int team2Members = NetworkRoomManagerExt.Instance.team2.Count;

            int id;
            if (team1Members <= team2Members)
            {
                id = 1;
            
            }
            else
            {
                id = 2;
            }

            CmdSetTeamID(id);
        }

        private void ChangeTeam1()
        {
            CmdSetTeamID(1);
        }
    
        private void ChangeTeam2()
        {
            CmdSetTeamID(2);
        }

        #endregion

        #region Mirror Callbacks

        public override void OnStartAuthority()
        {
            base.OnStartAuthority();
        
            SetUsername(PlayerNameInput.DisplayName);
            if (index == 0) IsLeader = true;
        }

        public override void OnStartClient()
        {
            base.OnStartClient();
            AddToTeamWithLessMembers();
        }

        public override void OnClientEnterRoom()
        {
            base.OnClientEnterRoom();
        
            LobbyDisplay.Instance.UpdateDisplay();
        }

        public override void OnClientExitRoom()
        {
            base.OnClientExitRoom();

            LobbyDisplay.Instance.UpdateDisplay();
        }
        #endregion
    }
}
