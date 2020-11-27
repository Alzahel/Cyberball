using System;
using Mirror;
using UnityEngine;

namespace Health
{
    [RequireComponent(typeof(HealthSystem))]
    public class Damageable : NetworkBehaviour
    {
        private HealthSystem health;

        private void Awake()
        {
            health = GetComponent<HealthSystem>();
        }

        [Server]
        public void Damage(int damageAmount)
        {
            health.Remove(damageAmount);
        }
    }
}
