using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGraphics : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem muzzleFlash = null;
    [SerializeField]
    private GameObject hitEffectPrefab = null;

    public ParticleSystem MuzzleFlash { get => muzzleFlash; }
    public GameObject HitEffectPrefab { get => hitEffectPrefab; }
}
