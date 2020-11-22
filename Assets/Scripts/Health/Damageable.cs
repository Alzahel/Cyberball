using Mirror;
using UnityEngine;

namespace Cyberball.Health
{
    [RequireComponent(typeof(Health))]
    public class Damageable : NetworkBehaviour
    {
        [SerializeField] private Health health = null;

        public void Damage(int _damageAmount)
        {
            health.Remove(_damageAmount);
        }
    }
}
