using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class NetworkGamePlayer : NetworkBehaviour
    {

        #region player Datas

        [SyncVar] private string username;
        [SyncVar] private int teamID;

        public int TeamID { get => teamID; set => teamID = value; }
        public string Username { get =>username; set => username = value; }

        #endregion 

        #region Loading game

        [SyncVar] private bool hasLoaded = false;

        public bool HasLoaded { get => hasLoaded; set => hasLoaded = value; }
        


        //We add the player to the list of players;
        public override void OnStartServer()
        {
            base.OnStartServer();

            GameManager.instance.Players.Add(this);
        }

        #endregion
    }
}