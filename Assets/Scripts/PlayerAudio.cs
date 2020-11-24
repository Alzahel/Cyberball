using System;
using Managers;
using Mirror;
using UnityEngine;

public class PlayerAudio : NetworkBehaviour
{
    private AudioManager audioManager;
    private PlayerMovement playerMovement;
    private PlayerShoot playerShoot;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerShoot = GetComponent<PlayerShoot>();
        audioManager = AudioManager.Instance;
    }

    #region Event Subscribing

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();

        if (playerMovement != null)
        { 
            playerMovement.OnSprint += PlaySprintAudio;
        }

        if (playerShoot != null)
        {
            playerShoot.Shot += CmdPlayShootAudio;
            playerShoot.Hit += CmdPlayHitAudio;
        }
    }

    public override void OnStopAuthority()
    {
        base.OnStopAuthority();
        
        if (playerMovement != null)
        {
            playerMovement.OnSprint -= PlaySprintAudio;
        }
        if (playerShoot != null)
        {
            playerShoot.Shot -= CmdPlayShootAudio;
            playerShoot.Hit -= CmdPlayHitAudio;
        }
        
    }

    #endregion

    #region Playing Audios

    private void PlaySprintAudio(object sender, PlayerMovement.OnSprintEventArgs e)
    {
        audioManager.PlaySound(e.IsSprinting ? "LoadSprint" : "UnloadSprint");
    }
    
    [Command]
    private void CmdPlayShootAudio(object sender,  PlayerShoot.OnShootEventArgs e)
    {
        RpcPlayShootAudio(e.Position);
    }
    
    [ClientRpc]
    private void RpcPlayShootAudio(Vector3 position)
    {
        audioManager.PlaySpatialSound("Shoot", position);
    }
    
    [Command]
    private void CmdPlayHitAudio(object sender,  PlayerShoot.OnHitEventArgs e)
    {
        RpcPlayHitAudio(e.HitTag);
    }
    
    [ClientRpc]
    private void RpcPlayHitAudio(string hitTag)
    {
        if (hitTag != GameManager.PlayerTag || hitTag != GameManager.PlayerHeadTag) return;
        
        audioManager.PlaySound(hitTag == GameManager.PlayerTag ? "BulletHit" : "BulletCriticalHit");
    }
    
    #endregion

}