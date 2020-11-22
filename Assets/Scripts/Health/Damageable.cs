using Mirror;
using UnityEngine;

namespace Health
{
    [RequireComponent(typeof(Health))]
    public class Damageable : NetworkBehaviour
    {
        [SerializeField] private Health health;

        public void Damage(int damageAmount)
        {
            health.Remove(damageAmount);
        }
    }
}
