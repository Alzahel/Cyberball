using Mirror;
using UnityEngine;

public class HostRoom : MonoBehaviour
{
    [SerializeField] private NetworkRoomManager networkManager;

    [Header("UI")]
    [SerializeField] private GameObject landingPagePanel;
    [SerializeField] private GameObject lobbyPanel;

    public void StartHost()
    {
        networkManager.StartHost();

        if (!networkManager.isNetworkActive) return;

        landingPagePanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }
}