using Mirror;
using UnityEngine;
using Weapons;

public class PlayerVisualEffects : NetworkBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerShoot playerShoot;
    private WeaponManager weaponManager;
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerShoot = GetComponent<PlayerShoot>();
        weaponManager = GetComponent<WeaponManager>();
    }
    #region Event Subscribing

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();

        if (playerMovement != null)
        { 
            //playerMovement.OnSprint += PlaySprintAudio;
        }

        if (playerShoot != null)
        {
            if(weaponManager != null) playerShoot.Shot += CmdSpawnShotEffect;
            playerShoot.Hit += CmdSpawnHitEffect;
        }
    }

    public override void OnStopAuthority()
    {
        base.OnStopAuthority();
        
        if (playerMovement != null)
        {
            //playerMovement.OnSprint -= PlaySprintAudio;
        }
        if (playerShoot != null)
        {
            if(weaponManager != null) playerShoot.Shot -= CmdSpawnShotEffect;
            playerShoot.Hit -= CmdSpawnHitEffect;
        }
        
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