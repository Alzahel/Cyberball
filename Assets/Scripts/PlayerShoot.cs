using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    [RequireComponent(typeof(WeaponManager))]
    public class PlayerShoot : NetworkBehaviour
    {
        private WeaponManager weaponManager;
        private PlayerWeapon weapon;

        private Camera cam;

        Vector3 rayOrigin;
        private float nextFire;
        [SerializeField]
        private LayerMask hitLayerMask = default;

        private bool shooting = false;

        private const string PLAYER_TAG = "Player";
        private const string PLAYER_HEAD_TAG = "PlayerHead";

        public delegate void OnShoot();
        public event OnShoot Shot = delegate { };


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
                if (shooting == true) Shoot();
            }

        }
        private void OnReload()
        {
            if (hasAuthority && !weaponManager.IsReloading) weaponManager.Reload();

        }

        private void OnFire1(InputValue value)
        {
            if (hasAuthority)
            {
                if (value.Get<float>() == 1)
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
                if (weaponManager.IsReloading && value.Get<float>() == 1 && weaponManager.CurrentWeapon.RemainingAmmunitions > 0)
                {
                    weaponManager.cancelReload();
                }
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

                if (Shot != null) Shot.Invoke();
                weapon.RemainingAmmunitions--;
                Debug.Log(weapon.RemainingAmmunitions);
                //We are shooting, call the OnShoot method on the server
                CmdOnShoot();

                RaycastHit hit;

                //if we hit something
                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weapon.Range, hitLayerMask))
                {
                    if (hit.collider.tag == PLAYER_HEAD_TAG)
                    {
                        CmdPlayerShot(hit.collider.transform.root.GetComponent<NetworkIdentity>().netId, weapon.HeadShotDamages, netId);
                        Debug.Log("headshot ! Hit on " + hit.collider.transform.root.name);
                    }
                    else if (hit.collider.tag == PLAYER_TAG)
                    {
                        CmdPlayerShot(hit.collider.GetComponent<NetworkIdentity>().netId, weapon.Damages, netId);
                        Debug.Log("hit " + hit.collider.transform.root.name);
                    }
                    //call the Onhit method on the server when we hit a surface
                    Debug.Log("hit");
                    CmdOnHit(hit.point, hit.normal, hit.collider.tag);
                }
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
            if (AudioManager.instance != null) AudioManager.instance.PlaySpatialSound("GunSound", transform.position);
            weaponManager.CurrentGraphics.MuzzleFlash.Play();
        }

        //Is called on the server when a bullet hit something
        [Command]
        private void CmdOnHit(Vector3 _pos, Vector3 _normal, string _hitTag)
        {
            RPCHitEffect(_pos, _normal, _hitTag);
        }

        //is Call on all client to sawn the hit effect
        [ClientRpc]
        private void RPCHitEffect(Vector3 _pos, Vector3 _normal, string _hitTag)
        {
            Debug.Log("hitEffect");
            if (_hitTag == PLAYER_TAG) AudioManager.instance.PlaySound("BulletHit");
            if (_hitTag == PLAYER_HEAD_TAG) AudioManager.instance.PlaySound("BulletCriticalHit");
            GameObject _hitEffectIns = Instantiate(weaponManager.CurrentGraphics.HitEffectPrefab, _pos, Quaternion.LookRotation(_normal));
            Destroy(_hitEffectIns, 2f);
        }

        [Command]
        void CmdPlayerShot(uint _damagedPlayerID, int _damage, uint _damageSourceID)
        {
            Debug.Log(_damagedPlayerID + " has been shot.");
            //Player player = GameManager.GetPlayer(_damagedPlayerID);
            //player.RPCTakeDamage(_damage, _damageSourceID);

        }
    }
}