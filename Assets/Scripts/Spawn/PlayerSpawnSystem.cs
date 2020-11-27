using System.Collections;
using System.Collections.Generic;
using Managers;
using Mirror;
using Mirror.Examples.Chat;
using Network;
using UnityEngine;
using UnityEngine.UIElements;

namespace Cyberball.Spawn
{
    public class PlayerSpawnSystem : NetworkBehaviour
    {
        public static PlayerSpawnSystem Instance;

        private void Awake()
        {
            if (Instance == null) Instance = this;

        }

        #region Register Spawn Points

        private static readonly List<PlayerSpawnPoint> SpawnPoints = new List<PlayerSpawnPoint>();

        public static void AddSpawnPoint(PlayerSpawnPoint spawnPoint)
        {
            SpawnPoints.Add(spawnPoint);
        }

        public static void RemoveSpawnPoint(PlayerSpawnPoint spawnPoint) => SpawnPoints.Remove(spawnPoint);

        #endregion

        #region Spawn Players

        [Server]
        public void SpawnAllPlayers(float timeBeforeRespawn)
        {
            foreach (var player in GameManager.Instance.Players)
            {
                var spawnPosition = GetSpawnPos(player.TeamID);
                SpawnPlayer(player.gameObject, spawnPosition, timeBeforeRespawn);
            }
        }

        // We send the order to the client that own authority on the player to set his position to spawn pos
        [ClientRpc]
        public void SpawnPlayer(GameObject player, Transform spawnPosition, float timeBeforeRespawn)
        {
           // NetworkGamePlayer player = conn.identity.GetComponent<NetworkGamePlayer>();
            StartCoroutine(Spawn(player, spawnPosition, timeBeforeRespawn));
        }
        
        private IEnumerator Spawn(GameObject player,Transform spawnPosition, float timeBeforeRespawn)
        {
            yield return new WaitForSeconds(timeBeforeRespawn);
            
            if (spawnPosition != null)
            {
                player.transform.SetPositionAndRotation(spawnPosition.position, spawnPosition.rotation);
                player.gameObject.SetActive(true);
                player.GetComponent<NetworkGamePlayer>().Respawn();
            }
        }
        
        
        public Transform GetSpawnPos(int teamID)
        {
            PlayerSpawnPoint spawnPoint = null;

            foreach (PlayerSpawnPoint _spawnPoint in SpawnPoints)
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
        

        #endregion
    }
}