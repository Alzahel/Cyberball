using System;
using Mirror;
using UnityEngine;

namespace Health
{
    public class HealthSystem : NetworkBehaviour
    {
        

        [SerializeField] private float maxHealth = 100f;

        [SyncVar(hook = nameof(HandleHealthChanged))]
        private float currentHealth;

        public static event EventHandler<DeathEventArgs> OnDeath;
        public event EventHandler<HealthChangedEventArgs> OnHealthChanged;
        
        public class HealthChangedEventArgs
        {
            public float MaxHealth;
            public float CurrentHealth;
        }
        public class DeathEventArgs
        {
            public NetworkConnection ConnectionToClient;
        }

        public bool IsDead => currentHealth == 0f;

        public override void OnStartServer()
        {
            ResetHealth();
        }

        [ServerCallback]
        private void OnDestroy()
        {
            OnDeath?.Invoke(this, new DeathEventArgs { ConnectionToClient = connectionToClient });
        }
        
        public float GetHealthPercent()
        {
            return Mathf.Clamp01(currentHealth / maxHealth);
        }

        [Server]
        public void Add(float value)
        {
            currentHealth += value;
            
            ClampHealth();
        }
    
        [Server]
        public void Remove(float value)
        {
            currentHealth -= value;
            
            ClampHealth();
            
            if (currentHealth <= 0)
            {
                OnDeath?.Invoke(this, new DeathEventArgs { ConnectionToClient = connectionToClient });

                RPCHandleDeath();
            } 
        }
        
        public void ResetHealth()
        {
            currentHealth = maxHealth;
        }

        private void HandleHealthChanged(float oldValue, float newValue)
        {
            OnHealthChanged?.Invoke(this, new HealthChangedEventArgs
        {
            MaxHealth = maxHealth,
            CurrentHealth = currentHealth
        });
        }

        [ClientRpc]
        private void RPCHandleDeath()
        {
            gameObject.SetActive(false);
        }

        private void ClampHealth()
        {
            Mathf.Clamp(currentHealth, 0, maxHealth);
        }
    }
}
