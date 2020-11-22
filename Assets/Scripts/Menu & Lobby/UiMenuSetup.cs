using UnityEngine;

public class UiMenuSetup : MonoBehaviour
{
    [SerializeField] private GameObject panelNameInput;
    [SerializeField] private GameObject panelLandingPage;
    [SerializeField] private GameObject panelEnterIpAddress;
    [SerializeField] private GameObject panelLobby;

    
   
    private void Awake()
    {        
        panelNameInput.SetActive(true);
        panelLandingPage.SetActive(false);
        panelEnterIpAddress.SetActive(false);
        panelLobby.SetActive(false);
    }
}