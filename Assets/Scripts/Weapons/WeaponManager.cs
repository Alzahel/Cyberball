using System.Collections;
using Cyberball.Managers;
using Mirror;
using UnityEngine;

namespace Cyberball.Weapons
{ 
    public class WeaponManager : NetworkBehaviour
    {
        private string weaponLayerName = "Weapon";

        [SerializeField]
        private Transform weaponHolderRightHand = null;
        [SerializeField]
        private Transform weaponHolderLeftHand = null;
        private Transform weaponHolder;

        [SerializeField]
        private PlayerWeapon primaryWeapon = null;
        private PlayerWeapon secondaryWeapon;

        private PlayerWeapon currentWeapon;
        private WeaponGraphics currentGraphics;

        private bool isReloading = false;

        private Coroutine reloadCoroutine;

        GameObject _weaponIns;

        public PlayerWeapon CurrentWeapon { get => currentWeapon; }
        public WeaponGraphics CurrentGraphics { get => currentGraphics; }
        public bool IsReloading { get => isReloading; set => isReloading = value; }

        // Start is called before the first frame update
        void Start()
        {
            EquiWeapon(primaryWeapon);
        }

        private void EquiWeapon(PlayerWeapon _weapon)
        {
            currentWeapon = _weapon;
            if (_weapon.IsRightHand) weaponHolder = weaponHolderRightHand;
            else weaponHolder = weaponHolderLeftHand;

            _weaponIns = Instantiate(_weapon.Graphics, weaponHolder.position, _weapon.Graphics.transform.rotation);
            currentGraphics = _weaponIns.GetComponent<WeaponGraphics>();
            _weaponIns.transform.SetParent(weaponHolder, true);

            if (currentGraphics == null) Debug.LogError("No weapon graphics components on the object " + _weaponIns.name);

            if (hasAuthority) Utils.SetLayerRecursively.setLayer(_weaponIns, LayerMask.NameToLayer(weaponLayerName));
        }

        public void Reload()
        {
            if (isReloading) return;
            reloadCoroutine = StartCoroutine(Reloading());
        }

        public void cancelReload()
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
            if (AudioManager.instance != null) AudioManager.instance.PlaySpatialSound("Reload", transform.position);
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