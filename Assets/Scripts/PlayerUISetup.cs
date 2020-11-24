using System;
using Mirror;
using Network;
using TMPro;
using UnityEngine;

public class PlayerUISetup : MonoBehaviour
{
    [SerializeField] private NetworkGamePlayer player;

    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject nameplate;
    
    private void Start()
    {
        hud.SetActive(false);
        nameplate.SetActive(true);
        
        nameplate.GetComponentInChildren<TextMeshProUGUI>().text = player.Username;

        if (player.hasAuthority) ActivateLocalPlayerUI();
    }

    private void ActivateLocalPlayerUI()
    {
        hud.SetActive(true);
        nameplate.SetActive(false);
    }
}