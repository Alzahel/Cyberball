using UnityEngine;

namespace Cyberball
{
    public class UiMenuSetup : MonoBehaviour
    {
        [SerializeField] private GameObject panelNameInput = null;
        [SerializeField] private GameObject panelLandingPage = null;
        [SerializeField] private GameObject panelEnterIpAddress = null;
        [SerializeField] private GameObject panelLobby = null;

    
   
        private void Awake()
        {        
            panelNameInput.SetActive(true);
            panelLandingPage.SetActive(false);
            panelEnterIpAddress.SetActive(false);
            panelLobby.SetActive(false);
        }
    }
}
