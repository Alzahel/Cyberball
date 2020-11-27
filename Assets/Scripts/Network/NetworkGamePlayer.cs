using System;
using Health;
using Managers;
using Mirror;
using UnityEngine;
using Weapons;

namespace Network
{
    public class NetworkGamePlayer : NetworkBehaviour
    {

        #region player Datas

        //score
        [SyncVar] private int kills;
        [SyncVar] private int deaths;

        public event EventHandler OnRespawn;

        [field: SyncVar] public int TeamID { get; set; }

        [field: SyncVar] public string Username { get; set; }

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

        public void Respawn()
        {
            OnRespawn?.Invoke(this, EventArgs.Empty);
        }
    }
}
