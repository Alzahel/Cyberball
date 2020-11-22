using UnityEngine;

namespace Cyberball.Health
{
    [RequireComponent(typeof(global::Health.Health))]
    public class Healable : MonoBehaviour
    {
        private global::Health.Health health;

        private void Awake()
        {
            health = GetComponent<global::Health.Health>();
        }

        public void Heal(int healAmount)
        {
            health.Remove(healAmount);
        }
    }
}
