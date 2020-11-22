using System.Collections.Generic;
using Managers;
using Mirror;
using Network;
using UnityEngine;

namespace Cyberball.Spawn
{
    public class PlayerSpawnSystem : NetworkBehaviour
    {
        public static PlayerSpawnSystem instance;

        private void Awake()
        {
            if (instance = null) instance = this;

        }

        #region Register Spawn Points

        private static List<PlayerSpawnPoint> spawnPoints = new List<PlayerSpawnPoint>();

        public static void AddSpawnPoint(PlayerSpawnPoint _spawnPoint)
        {
            spawnPoints.Add(_spawnPoint);
        }

        public static void RemoveSpawnPoint(PlayerSpawnPoint _spawnPoint) => spawnPoints.Remove(_spawnPoint);

        #endregion

        #region Spawn Players

        public void SpawnAllPlayers()
        {
            foreach (NetworkGamePlayer player in GameManager.Instance.Players)
            {
                SpawnPlayer(player.GetComponent<NetworkIdentity>().connectionToClient);
            }
        }

        public Transform getSpawnPos(int teamID)
        {
            PlayerSpawnPoint spawnPoint = null;

            foreach (PlayerSpawnPoint _spawnPoint in spawnPoints)
            {
                if (_spawnPoint.TeamID == teamID) spawnPoint = _spawnPoint;
            }
            if (spawnPoint == null)
            {
                Debug.LogError($"Missing spawn point for team {teamID}");
                return null;
            }

            return spawnPoint.transform;
        }

        // We send the order to the client that own authority on the player to set his position to spawn pos
        [TargetRpc]
        public void SpawnPlayer(NetworkConnection conn)
        {
            NetworkGamePlayer player = conn.identity.GetComponent<NetworkGamePlayer>();

            Transform spawnPosition = getSpawnPos(player.TeamID);

             if(spawnPosition != null)
            {
                player.transform.position = spawnPosition.position;
                player.transform.rotation = spawnPosition.rotation;
                player.gameObject.SetActive(true);
            }
        }

        #endregion
    }
}