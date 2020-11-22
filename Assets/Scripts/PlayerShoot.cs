using System;
using Cyberball.Weapons;
using Managers;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(WeaponManager))]
public class PlayerShoot : NetworkBehaviour
{
    private WeaponManager weaponManager;
    private PlayerWeapon weapon;

    private UnityEngine.Camera cam;

    Vector3 rayOrigin;
    private float nextFire;
    [SerializeField]
    private LayerMask hitLayerMask = default;

    private bool shooting = false;

    private const string PlayerTag = "Player";
    private const string PlayerHeadTag = "PlayerHead";

    public delegate void OnShoot();
    public event OnShoot Shot = delegate { };


    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<UnityEngine.Camera>();
        weaponManager = GetComponent<WeaponManager>();
    }

    // Update is called once per frame
    void Update()
    {
        weapon = weaponManager.CurrentWeapon;

        if (hasAuthority)
        {
            if (shooting == true) Shoot();
        }

    }
    private void OnReload()
    {
        if (hasAuthority && !weaponManager.IsReloading) weaponManager.Reload();

    }

    private void OnFire1(InputValue value)
    {
        if (!hasAuthority) return;
        
        var isKeyPushed = Math.Abs(value.Get<float>()) >= 1;
        
        if (isKeyPushed)
        {
            //if it's a single shot weapon the player has to click again to trigger a new shoot, if it's not we set shooting to true and it will keep firing while
            //the OnFire button keep pressed
            if (!weapon.SingleShot)
            {
                shooting = true;
            }
            else Shoot();
        }
        else shooting = false;

        //cancel reload if shooting during reloading
        if (weaponManager.IsReloading && isKeyPushed && weaponManager.CurrentWeapon.RemainingAmmunitions > 0)
        {
            weaponManager.cancelReload();
        }
    }

    [Client]
    private void Shoot()
    {
        //Can't shoot if there is no remaining bullet, launch the reload
        if (weapon.RemainingAmmunitions <= 0)
        {
            Debug.Log("Out of bullets");
            weaponManager.Reload();
            return;
        }

        float time = Time.time;
        if (time > nextFire)
        {
            nextFire = time + weapon.FireRate;

            Shot?.Invoke();
            weapon.RemainingAmmunitions--;
            Debug.Log(weapon.RemainingAmmunitions);
            //We are shooting, call the OnShoot method on the server
            CmdOnShoot();

            RaycastHit hit;

            //if we hit something
            if (!Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weapon.Range,
                hitLayerMask)) return;
            
            var hitCollider = hit.collider;
            var hitRootObject = hitCollider.transform.root;
            var hitNetID = hitCollider.GetComponent<NetworkIdentity>().netId;
                
            if (hit.collider.CompareTag(PlayerHeadTag))
            {
                CmdPlayerShot(hitNetID, weapon.HeadShotDamages, netId);
                Debug.Log("headshot ! Hit on " + hitRootObject.name);
            }
            else if (hit.collider.CompareTag(PlayerTag))
            {
                CmdPlayerShot(hitNetID, weapon.Damages, netId);
                Debug.Log("hit " + hitRootObject.name);
            }
            //call the Onhit method on the server when we hit a surface
            Debug.Log("hit");
            CmdOnHit(hit.point, hit.normal, hitCollider.tag);
        }
    }

    //Called on the server when the player shoot
    [Command]
    private void CmdOnShoot()
    {
        RPCShotEffect();
    }

    [ClientRpc]
    private void RPCShotEffect()
    {
        if (AudioManager.Instance != null) AudioManager.Instance.PlaySpatialSound("GunSound", transform.position);
        weaponManager.CurrentGraphics.MuzzleFlash.Play();
    }

    //Is called on the server when a bullet hit something
    [Command]
    private void CmdOnHit(Vector3 pos, Vector3 normal, string hitTag)
    {
        RPCHitEffect(pos, normal, hitTag);
    }

    //is Call on all client to sawn the hit effect
    [ClientRpc]
    private void RPCHitEffect(Vector3 pos, Vector3 normal, string hitTag)
    {
        Debug.Log("hitEffect");
        if (hitTag == PlayerTag) AudioManager.Instance.PlaySound("BulletHit");
        if (hitTag == PlayerHeadTag) AudioManager.Instance.PlaySound("BulletCriticalHit");
        GameObject hitEffectIns = Instantiate(weaponManager.CurrentGraphics.HitEffectPrefab, pos, Quaternion.LookRotation(normal));
        Destroy(hitEffectIns, 2f);
    }

    [Command]
    void CmdPlayerShot(uint damagedPlayerID, int damage, uint damageSourceID)
    {
        Debug.Log(damagedPlayerID + " has been shot.");
        //Player player = GameManager.GetPlayer(_damagedPlayerID);
        //player.RPCTakeDamage(_damage, _damageSourceID);

    }
}