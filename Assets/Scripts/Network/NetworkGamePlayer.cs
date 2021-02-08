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
        [SyncVar] private int goals;

        public int Kills
        {
            get => kills;
            set => kills = value;
        }

        public int Deaths
        {
            get => deaths;
            set => deaths = value;
        }
        
        public int Goals
        {
            get => goals;
            set => goals = value;
        }
        

        public event EventHandler OnRespawn;

        [field: SyncVar] public int TeamID { get; set; }

        [field: SyncVar] public string Username { get; set; }

        #endregion 

        #region Loading game

        [SyncVar] private bool hasLoaded;

        public bool HasLoaded { get => hasLoaded; set => hasLoaded = value; }

        
        //We add the player to the list of players;
        public override void OnStartClient()
        {
                GameManager.Instance.Players.Add(this);

                gameObject.name = Username;
        }

        #endregion

        public void Respawn()
        {
            OnRespawn?.Invoke(this, EventArgs.Empty);
        }
    }
}
