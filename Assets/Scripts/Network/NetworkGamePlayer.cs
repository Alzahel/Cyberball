using System;
using Managers;
using Mirror;
using UnityEngine;

namespace Network
{
    public class NetworkGamePlayer : NetworkBehaviour
    {

        #region player Datas

        [SyncVar] private string username;
        [SyncVar] public int teamID;
        
        //score
        [SyncVar] private int kills;
        [SyncVar] private int deaths;

        private bool isDead;

        public event EventHandler OnAuthorityChanged;

        public int TeamID { get => teamID; set => teamID = value; }
        public string Username { get =>username; set => username = value; }
        public bool IsDead { get => isDead; set => isDead = value; }

        #endregion 

        #region Loading game

        [SyncVar] private bool hasLoaded;

        public bool HasLoaded { get => hasLoaded; set => hasLoaded = value; }

        //We add the player to the list of players;
        public override void OnStartServer()
        {
            base.OnStartServer();

            GameManager.Instance.Players.Add(this);
        }

        #endregion

        #region Authority

        public override void OnStartAuthority()
        {
            OnAuthorityChanged?.Invoke(this, EventArgs.Empty);
        }

        public override void OnStopAuthority()
        {
            OnAuthorityChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
