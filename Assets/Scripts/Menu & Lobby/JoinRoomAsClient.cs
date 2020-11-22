using Mirror;
using Network;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinRoomAsClient : MonoBehaviour
{
    [SerializeField] private NetworkRoomManager networkManager;
    
    [Header("UI")]
    [SerializeField] private GameObject enterIpAddressPanel;
    [SerializeField] private GameObject lobbyPanel;
    [SerializeField] private TMP_InputField ipAddressInputField;
    [SerializeField] private Button joinButton;

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

    //If the connection is successful we switch to lobby Panel
    private void HandleClientConnected()
    {
        enterIpAddressPanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }

    //If the connection isn't successful or the player leaves the room we come back to the enter ip panel
    private void HandleClientDisconnected()
    {
        joinButton.interactable = true;

        lobbyPanel.SetActive(false);
        enterIpAddressPanel.SetActive(true);
    }
}