using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
