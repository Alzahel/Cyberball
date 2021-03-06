﻿using UnityEngine;

namespace Cyberball.Weapons
{
    public class WeaponGraphics : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem muzzleFlash = null;
        [SerializeField]
        private GameObject hitEffectPrefab = null;

        public ParticleSystem MuzzleFlash { get => muzzleFlash; }
        public GameObject HitEffectPrefab { get => hitEffectPrefab; }
    }
}
