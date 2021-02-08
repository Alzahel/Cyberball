using System;
using Cyberball.Weapons;
using Health;
using JetBrains.Annotations;
using Managers;
using Mirror;
using Mirror.SimpleWeb;
using Network;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Weapons;

[RequireComponent(typeof(WeaponManager))]
public class PlayerShoot : NetworkBehaviour
{
    private WeaponManager weaponManager;
    private PlayerWeapon weapon;

    private Camera cam;

    private Vector3 rayOrigin;
    private float nextFire;
    [SerializeField]
    private LayerMask hitLayerMask;

    private bool shooting;
    
    public event EventHandler<OnShootEventArgs> Shot;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 Position;
    }
    
    public event EventHandler<OnHitEventArgs> Hit;
    public class OnHitEventArgs : EventArgs
    {
        public Vector3 Position;
        public string HitTag;
        public Vector3 HitNormal;
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<Camera>();
        weaponManager = GetComponent<WeaponManager>();
    }

    // Update is called once per frame
    void Update()
    {
        weapon = weaponManager.CurrentWeapon;

        if (hasAuthority)
        {
            if (shooting) Shoot();
            
            if(Input.GetKeyDown(KeyCode.K)) CmdPlayerShot(this.gameObject, 50);
        }

    }
    
    [UsedImplicitly]
    private void OnReload()
    {
        if (hasAuthority && !weaponManager.IsReloading) weaponManager.Reload();

    }

    [UsedImplicitly]
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
            weaponManager.CancelReload();
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

            
            
            weapon.RemainingAmmunitions--;
            Debug.Log(weapon.RemainingAmmunitions);

            //We are shooting, call the OnShoot method on the server
            OnShoot();
        }
    }

    //Call the event that the player has shot to apply appropriate effects 
    private void OnShoot()
    {
        Shot?.Invoke(this, new OnShootEventArgs{Position = transform.position});
        
        RaycastHit hit;

        //if we hit something
        if (!Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weapon.Range, hitLayerMask)) 
            return;

        OnHit(hit);
    }

    //Is called  when a bullet hit something
    private void OnHit(RaycastHit hit)
    {
        var hitCollider = hit.collider;
        var hitRootObject = hitCollider.transform.root.gameObject;
        
        Hit?.Invoke(this, new OnHitEventArgs{Position = hit.point, HitTag = hitCollider.tag, HitNormal = hit.normal});
        
        if (hitCollider.gameObject.layer!= LayerMask.NameToLayer($"Player")) return;

        if (hit.collider.CompareTag(GameManager.PlayerHeadTag))
        {
            if(hitRootObject.GetComponent<HealthSystem>().IsDead) return;
            
            CmdPlayerShot(hitRootObject, weapon.HeadShotDamages);
            Debug.Log("headshot ! Hit on " + hitRootObject.name);
        }
        else if (hit.collider.CompareTag(GameManager.PlayerTag))
        {
            if(hitRootObject.GetComponent<HealthSystem>().IsDead) return;
            
            CmdPlayerShot(hitRootObject, weapon.Damages);
            Debug.Log("hit " + hitRootObject.name);
        }
    }

    [Command]
    void CmdPlayerShot(GameObject hitGameObject, int damage)
    {
        Debug.Log(hitGameObject.name + " has been shot.");
        
        hitGameObject.GetComponent<Damageable>().Damage(damage, gameObject);
    }
}