using UnityEngine;

namespace Health
{
    [RequireComponent(typeof(global::Health.HealthSystem))]
    public class Healable : MonoBehaviour
    {
        private global::Health.HealthSystem healthSystem;

        private void Awake()
        {
            healthSystem = GetComponent<HealthSystem>();
        }

        public void Heal(int healAmount)
        {
            healthSystem.Remove(healAmount);
        }
    }
}
