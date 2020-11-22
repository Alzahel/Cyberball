using System;
using System.Collections.Generic;
using System.Linq;
using Cyberball.Spawn;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace Network
{
    public class NetworkRoomManagerExt : NetworkRoomManager
    {
        public static event Action OnClientConnected;
        public static event Action OnClientDisconnected;

        public static NetworkRoomManagerExt Instance;

        public List<GameObject> team1 = new List<GameObject>();
        public List<GameObject> team2 = new List<GameObject>();

        private Button startButton;

        [SerializeField] GameObject spawnSystem;

        public override void Awake()
        {
            base.Awake();

            if (Instance == null) Instance = this;
        }


        //Register prefabs that can be spawned into the network manager
        public override void OnStartServer() => spawnPrefabs = Resources.LoadAll<GameObject>("Prefabs/NetworkSpawned").ToList();
        public override void OnStartClient()
        {
            var spawnablePrefabs = Resources.LoadAll<GameObject>("Prefabs/NetworkSpawned");

            foreach (var prefab in spawnablePrefabs)
            {
                ClientScene.RegisterPrefab(prefab);
            }
        }

        public override void OnServerConnect(NetworkConnection conn)
        {
            base.OnServerConnect(conn);

            startButton = UiLobbySetup.Instance.startButton;
            startButton.onClick.AddListener(StartGame);
        }

        //Called if connected successfully
        public override void OnClientConnect(NetworkConnection conn)
        {
            base.OnClientConnect(conn);

            OnClientConnected?.Invoke();
        }

        //Called if disconnected or couldn't connect
        public override void OnClientDisconnect(NetworkConnection conn)
        {
            base.OnClientDisconnect(conn);

            OnClientDisconnected?.Invoke();
        }

        public override void OnRoomServerPlayersReady()
        {
            startButton.interactable = true;
        }


        //Override the creation of gameplayer to use our own SpawnSystem with custom positions
        public override GameObject OnRoomServerCreateGamePlayer(NetworkConnection conn, GameObject roomPlayer)
        {
            GameObject playerSpawnSystemInstance = Instantiate(spawnSystem);
            NetworkServer.Spawn(playerSpawnSystemInstance);

            PlayerSpawnSystem.instance = playerSpawnSystemInstance.GetComponent<PlayerSpawnSystem>();

            Transform spawnPosition = PlayerSpawnSystem.instance.getSpawnPos(roomPlayer.GetComponent<NetworkRoomPlayerExt>().TeamID);
            //spawnPosition.rotation = new Quaternion(spawnPosition.rotation.x, -spawnPosition.rotation.y, spawnPosition.rotation.z, spawnPosition.rotation.w); 
            GameObject gamePlayer = spawnPosition != null
                ? Instantiate(playerPrefab, spawnPosition.position, spawnPosition.rotation)
                : Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

            return gamePlayer;
        }

        //When the player finished to load the scene we set has loaded to true
        public override bool OnRoomServerSceneLoadedForPlayer(NetworkConnection conn, GameObject roomPlayer, GameObject gamePlayer)
        {
            NetworkRoomPlayerExt oldPlayer = roomPlayer.GetComponent<NetworkRoomPlayerExt>();
            NetworkGamePlayer player = gamePlayer.GetComponent<NetworkGamePlayer>();

            player.Username = oldPlayer.Username;
            player.TeamID = oldPlayer.TeamID;
            player.HasLoaded = true;

            return true;
        }

        private void StartGame()
        {
            ServerChangeScene(GameplayScene);
        }

    }
}
