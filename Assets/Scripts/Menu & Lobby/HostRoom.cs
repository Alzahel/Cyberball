using Mirror;
using UnityEngine;

namespace Cyberball
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