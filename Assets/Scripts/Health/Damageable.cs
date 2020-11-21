using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Damageable : NetworkBehaviour
{
    [SerializeField] private Health health = null;

    public void Damage(int _damageAmount)
    {
        health.Remove(_damageAmount);
    }
}
