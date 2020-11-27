using System;
using Cyberball;
using Cyberball.Spawn;
using Mirror;
using Network;
using UnityEngine;
using Weapons;

namespace Health
{
    public class HealthSystem : NetworkBehaviour
    {
        

        [SerializeField] private float maxHealth = 100f;

        [SyncVar(hook = nameof(HandleHealthChanged))]
        private float currentHealth;

        public event EventHandler<DeathEventArgs> OnDeath;
        public event EventHandler<HealthChangedEventArgs> OnHealthChanged;
        
        public class HealthChangedEventArgs
        {
            public float MaxHealth;
            public float CurrentHealth;
        }
        public class DeathEventArgs
        {
            public GameObject DeadObject;
        }

        public bool IsDead => currentHealth == 0f;

        public override void OnStartServer()
        {
            ResetHealth();
        }

        [ServerCallback]
        private void OnDestroy()
        {
            OnDeath?.Invoke(this, new DeathEventArgs { DeadObject = gameObject });
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
                HandleDeath();
            } 
        }
        
        [Server]
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
        
        [Server]
        private void HandleDeath()
        {
           
            
            RpcHandleDeath();
            PlayerSpawnSystem.Instance.SpawnPlayer(gameObject, PlayerSpawnSystem.Instance.GetSpawnPos(GetComponent<NetworkGamePlayer>().TeamID), MatchSettings.RespawnTime);
            GetComponent<HealthSystem>().ResetHealth();
            GetComponent<EnergySystem>().ResetEnergy();
            GetComponent<WeaponManager>().ResetAmmunition();
            
        }
        
        [ClientRpc]
        private void RpcHandleDeath()
        {
            OnDeath?.Invoke(this, new DeathEventArgs { DeadObject = gameObject });
            
            gameObject.SetActive(false);
            GetComponent<PlayerMovement>().CancelAllMovements();
        }

        private void ClampHealth()
        {
            Mathf.Clamp(currentHealth, 0, maxHealth);
        }
    }
}
