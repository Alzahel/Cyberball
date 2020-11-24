using UnityEngine;

namespace Cyberball.Health
{
    [RequireComponent(typeof(global::Health.HealthSystem))]
    public class Healable : MonoBehaviour
    {
        private global::Health.HealthSystem healthSystem;

        private void Awake()
        {
            healthSystem = GetComponent<global::Health.HealthSystem>();
        }

        public void Heal(int healAmount)
        {
            healthSystem.Remove(healAmount);
        }
    }
}
