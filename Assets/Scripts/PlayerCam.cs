using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Health;
using JetBrains.Annotations;
using Managers;
using Mirror;
using Network;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCam : NetworkBehaviour
{
    private HealthSystem healthSystem;
    [SerializeField] private CinemachineVirtualCamera playerCam;
    private GameObject currentActiveCam;

    private int camIndex = 0;
    private int firstCamIndex = 0;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        
    }
    
    void Start()
    {
        playerCam.gameObject.SetActive(hasAuthority);
        if (!hasAuthority) enabled = false;
        currentActiveCam = playerCam.gameObject;
    }


    private void OnEnable()
    {
        healthSystem.OnDeath += ChangeCamOnDeath;
    }

    private void OnDisable()
    {
        healthSystem.OnDeath -= ChangeCamOnDeath;
    }

    private void ChangeCamOnDeath(object sender, HealthSystem.DeathEventArgs e)
    {
        ChangeCam();
    }

    private void ChangeCam()
    {
        FindNextCamIndex();
        
        currentActiveCam.SetActive(false);
        GameManager.Instance.Players[camIndex].GetComponent<PlayerCam>().playerCam.gameObject.SetActive(true);
        currentActiveCam = GameManager.Instance.Players[camIndex].GetComponent<PlayerCam>().playerCam.gameObject;
        firstCamIndex = camIndex;
    }

    private void FindNextCamIndex()
    {
        camIndex++;
        
        if (camIndex >= GameManager.Instance.Players.Count) camIndex = 0;

        //if a whole loop is done and all player are dead then just stay on the actual cam
        if (camIndex == firstCamIndex) return;
        
        var playerToWatch = GameManager.Instance.Players[camIndex];
        if(playerToWatch.GetComponent<HealthSystem>().IsDead || playerToWatch.TeamID != GetComponent<NetworkGamePlayer>().TeamID) FindNextCamIndex();
    }

    public void RestorePlayerCam()
    {
        if (!hasAuthority) return;
        currentActiveCam.SetActive(false);
        currentActiveCam = playerCam.gameObject;
        playerCam.gameObject.SetActive(true);
        camIndex = 0;
    }

    [UsedImplicitly]
    private void OnChangeCam()
    {
        Debug.Log("changeCam");
        ChangeCam();
    }
    
}
