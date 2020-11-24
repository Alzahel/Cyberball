using System.Collections;
using Cyberball.Weapons;
using Managers;
using Mirror;
using UnityEngine;
using Utils;

namespace Weapons
{ 
    public class WeaponManager : NetworkBehaviour
    {
        private const string WeaponLayerName = "Weapon";

        [SerializeField] private Transform weaponHolderRightHand;
        [SerializeField] private Transform weaponHolderLeftHand;
        private Transform weaponHolder;

        [SerializeField] private PlayerWeapon primaryWeapon;
        private PlayerWeapon secondaryWeapon;

        private PlayerWeapon currentWeapon;
        private WeaponGraphics currentGraphics;

        private bool isReloading;

        private Coroutine reloadCoroutine;

        GameObject weaponIns;

        public PlayerWeapon CurrentWeapon { get => currentWeapon; }
        public WeaponGraphics CurrentGraphics { get => currentGraphics; }
        public bool IsReloading { get => isReloading; set => isReloading = value; }

        // Start is called before the first frame update
        void Start()
        {
            EquipWeapon(primaryWeapon);
        }

        private void EquipWeapon(PlayerWeapon weapon)
        {
            currentWeapon = weapon;
            if (weapon.IsRightHand) weaponHolder = weaponHolderRightHand;
            else weaponHolder = weaponHolderLeftHand;

            weaponIns = Instantiate(weapon.Graphics, weaponHolder.position, weapon.Graphics.transform.rotation);
            currentGraphics = weaponIns.GetComponent<WeaponGraphics>();
            weaponIns.transform.SetParent(weaponHolder, true);

            if (currentGraphics == null) Debug.LogError("No weapon graphics components on the object " + weaponIns.name);

            if (hasAuthority) SetLayerRecursively.setLayer(weaponIns, LayerMask.NameToLayer(WeaponLayerName));
        }

        public void Reload()
        {
            if (isReloading) return;
            reloadCoroutine = StartCoroutine(Reloading());
        }

        public void CancelReload()
        {
            IsReloading = false;
            StopCoroutine(reloadCoroutine);
            CMDOnCancelReload();
            Debug.Log("Cancel reload");
        }

        private IEnumerator Reloading()
        {
            isReloading = true;
            CMDOnReload();
            Debug.Log("Reloading...");
            yield return new WaitForSeconds(currentWeapon.ReloadTime);
            currentWeapon.RemainingAmmunitions = currentWeapon.MaxAmmunitions;
            isReloading = false;
        }

        [Command]
        private void CMDOnReload()
        {
            RPCOnReload();
        }

        [ClientRpc]
        private void RPCOnReload()
        {
            Animator anim = currentGraphics.GetComponentInChildren<Animator>();
            if (anim != null)
            {
                anim.SetTrigger("Reload");
            }
            if (AudioManager.Instance != null) AudioManager.Instance.PlaySpatialSound("Reload", transform.position);
        }

        [Command]
        private void CMDOnCancelReload()
        {
            RPCOnCancelReload();
        }

        [ClientRpc]
        private void RPCOnCancelReload()
        {
            Animator anim = currentGraphics.GetComponent<Animator>();
            if (anim != null)
            {
                anim.SetTrigger("cancelReload");
            }
        }
    }
}