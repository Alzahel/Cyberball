using Cyberball.Network;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cyberball
{
    public class JoinRoomAsClient : MonoBehaviour
    {
        [SerializeField] private NetworkRoomManager networkManager = null;

        [Header("UI")]
        [SerializeField] private GameObject enterIpAddressPannel = null;
        [SerializeField] private GameObject lobbyPanel = null;
        [SerializeField] private TMP_InputField ipAddressInputField = null;
        [SerializeField] private Button joinButton = null;

          private void OnEnable()
          {
              NetworkRoomManagerExt.OnClientConnected += HandleClientConnected;
              NetworkRoomManagerExt.OnClientDisconnected += HandleClientDisconnected;
          }
          private void OnDisable()
          {
              NetworkRoomManagerExt.OnClientConnected -= HandleClientConnected;
              NetworkRoomManagerExt.OnClientDisconnected -= HandleClientDisconnected;
          }

        public void JoinRoom()
        {
            string ipAddress = ipAddressInputField.text;

            networkManager.networkAddress = ipAddress;
            networkManager.StartClient();

            joinButton.interactable = false;
        }

        //If the connection is succesfull we switch to lobby Pannel
        private void HandleClientConnected()
        {
            enterIpAddressPannel.SetActive(false);
            lobbyPanel.SetActive(true);
        }

        //If the connection isn't succesfull or the player leaves the room we come back to the enter ip pannel
        private void HandleClientDisconnected()
        {
            joinButton.interactable = true;

            lobbyPanel.SetActive(false);
            enterIpAddressPannel.SetActive(true);
        }
    }
}