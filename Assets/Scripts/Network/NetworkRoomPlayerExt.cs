using Mirror;
using UnityEngine;

namespace Cyberball.Network
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
                UiLobbySetup.instance.startButton.gameObject.SetActive(value);
            }
        }

        #endregion

        #region SyncVar hooks

        public void HandleTeamChanged(int oldValue, int newValue) 
        { 
            LobbyDisplay.instance.UpdateDisplay();
            if (newValue == 1)
            {
                NetworkRoomManagerExt.instance.team1.Add(gameObject);
                if(oldValue ==2) NetworkRoomManagerExt.instance.team2.Remove(gameObject);
            }
            else
            {
                NetworkRoomManagerExt.instance.team2.Add(gameObject);
                if(oldValue == 1) NetworkRoomManagerExt.instance.team1.Remove(gameObject);
            }
        }

        public void HandleUsernameChanged(string oldValue, string newValue) => LobbyDisplay.instance.UpdateDisplay();
        public override void ReadyStateChanged(bool oldValue, bool newValue) => LobbyDisplay.instance.UpdateDisplay();
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
            UiLobbySetup.instance.readyButton.onClick.AddListener(SetReadyToBegin);
            UiLobbySetup.instance.changeTeam1.onClick.AddListener(ChangeTeam1);
            UiLobbySetup.instance.changeTeam2.onClick.AddListener(ChangeTeam2);
        }

        #endregion

        #region setSyncVars

        private void SetReadyToBegin()
        {
            CmdChangeReadyState(!readyToBegin);
        }

        [Command]
        private void setUsername(string _username)
        {
            username = _username;
        }

        [Command]
        private void CmdsetTeamID(int _teamID)
        {
            teamID = _teamID;
        }

        #endregion

        #region Team Management

        private void AddToTeamWithLessMembers()
        {
            int _team1Members = NetworkRoomManagerExt.instance.team1.Count;
            int _team2Members = NetworkRoomManagerExt.instance.team2.Count;

            int _teamID = 0;
            if (_team1Members <= _team2Members)
            {
                _teamID = 1;
                
            }
            else
            {
                _teamID = 2;
            }

           CmdsetTeamID(_teamID);
        }

        private void ChangeTeam1()
        {
            CmdsetTeamID(1);
        }
        
        private void ChangeTeam2()
        {
            CmdsetTeamID(2);
        }

        #endregion

        #region Mirror Callbacks

        public override void OnStartAuthority()
        {
            base.OnStartAuthority();
            
            setUsername(PlayerNameInput.DisplayName);
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
            
            LobbyDisplay.instance.UpdateDisplay();
        }

        public override void OnClientExitRoom()
        {
            base.OnClientExitRoom();

            LobbyDisplay.instance.UpdateDisplay();
        }
        #endregion
    }
}