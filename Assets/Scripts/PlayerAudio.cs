using System;
using Health;
using Managers;
using Mirror;
using Network;
using UnityEngine;

public class PlayerAudio : NetworkBehaviour
{
    private NetworkGamePlayer player;
    private AudioManager audioManager;
    private PlayerMovement playerMovement;
    private PlayerShoot playerShoot;
    private HealthSystem playerHealth;

    private void Awake()
    {
        player = GetComponent<NetworkGamePlayer>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShoot = GetComponent<PlayerShoot>();
        playerHealth = GetComponent<HealthSystem>();
        
        audioManager = AudioManager.Instance;
    }

    #region Event Subscribing

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();

        if (player != null) player.OnRespawn += CmdPlaySpawnAudio;
        
        if (playerMovement != null)
        { 
            playerMovement.OnSprint += PlaySprintAudio;
        }

        if (playerShoot != null)
        {
            playerShoot.Shot += CmdPlayShootAudio;
            playerShoot.Hit += CmdPlayHitAudio;
        }

        if (playerHealth != null) playerHealth.OnDeath += CmdPlayDeathAudio;
    }

    public override void OnStopAuthority()
    {
        base.OnStopAuthority();
        
        if (player != null) player.OnRespawn -= CmdPlaySpawnAudio;
        
        if (playerMovement != null)
        {
            playerMovement.OnSprint -= PlaySprintAudio;
        }
        if (playerShoot != null)
        {
            playerShoot.Shot -= CmdPlayShootAudio;
            playerShoot.Hit -= CmdPlayHitAudio;
        }
        
        if (playerHealth != null) playerHealth.OnDeath -= CmdPlayDeathAudio;
        
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
    
    [Command]
    private void CmdPlayDeathAudio(object sender,  HealthSystem.DeathEventArgs e)
    {
        RpcPlayDeathAudio(e.DeadObject.transform.position);
    }
    
    [ClientRpc]
    private void RpcPlayDeathAudio(Vector3 position)
    {
        audioManager.PlaySpatialSound("Death", position);
    }
    
    [Command]
    private void CmdPlaySpawnAudio(object sender, EventArgs e)
    {
        RpcPlaySpawnAudio();
    }
    
    [ClientRpc]
    private void RpcPlaySpawnAudio()
    {
        audioManager.PlaySpatialSound("Spawn", player.transform.position);
    }
    #endregion

}