using System;
using Cyberball.Weapons;
using TMPro;
using UnityEngine;
using Weapons;

namespace UI
{
    public class AmmunitionCount : MonoBehaviour
    {
        private WeaponManager weaponManager;
        private PlayerWeapon weapon;
        [SerializeField] private TextMeshProUGUI ammoCountText;

        private void Awake()
        {
            weaponManager = transform.root.GetComponent<WeaponManager>();
        }
        
        void Update()
        {
            weapon = weaponManager.CurrentWeapon;
            ammoCountText.text = weapon.RemainingAmmunitions.ToString();
        }
    }
}
