using System;
using Health;
using Mirror;
using Network;
using UnityEngine;
using Weapons;

public class PlayerVisualEffects : NetworkBehaviour
{
    private NetworkGamePlayer player;
    private PlayerMovement playerMovement;
    private PlayerShoot playerShoot;
    private WeaponManager weaponManager;
    private HealthSystem playerHealth;
    
    //prefab of the Death effect particles
    [SerializeField] private GameObject deathEffect;
    //prefab of the Death effect particles
    [SerializeField] private GameObject spawnEffect;
    
    private void Awake()
    {
        player = GetComponent<NetworkGamePlayer>();
        playerShoot = GetComponent<PlayerShoot>();
        weaponManager = GetComponent<WeaponManager>();
        playerHealth = GetComponent<HealthSystem>();
        
    }
    #region Event Subscribing

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();

        if (playerShoot != null)
        {
            if(weaponManager != null) playerShoot.Shot += CmdSpawnShotEffect;
            playerShoot.Hit += CmdSpawnHitEffect;
        }

        if (playerHealth != null) playerHealth.OnDeath += CmdSpawnDeathEffect;
        
        if (player != null) player.OnRespawn += CmdSpawnSpawnEffect;
    }

    public override void OnStopAuthority()
    {
        base.OnStopAuthority();
        
        if (playerShoot != null)
        {
            if(weaponManager != null) playerShoot.Shot -= CmdSpawnShotEffect;
            playerShoot.Hit -= CmdSpawnHitEffect;
        }
        
        if (playerHealth != null) playerHealth.OnDeath -= CmdSpawnDeathEffect;
    }
    
    [Command]
    private void CmdSpawnSpawnEffect(object sender, EventArgs e)
    {
        RpcSpawnSpawnEffect(sender, e);
    }
    
    
    [ClientRpc]
    private void RpcSpawnSpawnEffect(object sender, EventArgs e)
    {
        var spawnEffectIns = Instantiate(spawnEffect, player.transform.position, player.transform.rotation );
        Destroy(spawnEffectIns, 3f);
    }
    
    [Command]
    private void CmdSpawnDeathEffect(object sender, HealthSystem.DeathEventArgs e)
    {
        RpcSpawnDeathEffect(sender, e);
    }
    
    [ClientRpc]
    private void RpcSpawnDeathEffect(object sender, HealthSystem.DeathEventArgs e)
    {
        var DeathEffectIns = Instantiate(deathEffect, player.transform.position,Quaternion.identity );
        Destroy(DeathEffectIns, 3f);
    }

    [Command]
    private void CmdSpawnShotEffect(object sender, PlayerShoot.OnShootEventArgs e)
    {
        RpcSpawnShotEffect();
    }
    
    [ClientRpc]
    private void RpcSpawnShotEffect()
    {
        weaponManager.CurrentGraphics.MuzzleFlash.Play();
    }

    [Command]
    private void CmdSpawnHitEffect(object sender, PlayerShoot.OnHitEventArgs e)
    {
        RpcSpawnHitEffect(e);
    }
    
    [ClientRpc]
    private void RpcSpawnHitEffect(PlayerShoot.OnHitEventArgs e)
    {
        var hitEffectIns = Instantiate(weaponManager.CurrentGraphics.HitEffectPrefab, e.Position, Quaternion.LookRotation(e.HitNormal));
        Destroy(hitEffectIns, 2f);
    }

    #endregion
    
    
}