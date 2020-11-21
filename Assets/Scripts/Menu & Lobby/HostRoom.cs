using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Assets.LobbyImproved
{
    public class HostRoom : MonoBehaviour
    {
        [SerializeField] private NetworkRoomManager networkManager = null;

        [Header("UI")]
        [SerializeField] private GameObject landingPagePanel = null;
        [SerializeField] private GameObject lobbyPannel = null;

        public void StartHost()
        {
            networkManager.StartHost();

            if (!networkManager.isNetworkActive) return;

            landingPagePanel.SetActive(false);
            lobbyPannel.SetActive(true);
        }
    }
}