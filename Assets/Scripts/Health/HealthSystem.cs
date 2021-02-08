using System;
using System.Collections;
using Cyberball;
using Cyberball.Spawn;
using Managers;
using Mirror;
using Network;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;
using Object = UnityEngine.Object;

namespace Health
{
    public class HealthSystem : NetworkBehaviour
    {
        

        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private TextMeshProUGUI deathUI;

        [SyncVar(hook = nameof(HandleHealthChanged))]
        private float currentHealth;


        [SerializeField] private GameObject[] gameObjectsDesactivateOnDeath;
        [SerializeField] private Behaviour[] behavioursDesactivateOnDeath;
        public float TimeRemainingBeforeRespawn { get; set; } = 0;

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
            OnDeath?.Invoke(this, new DeathEventArgs { DeadObject = gameObject});
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
        public void Remove(float value, GameObject source)
        {
            currentHealth -= value;
            
            ClampHealth();
            
            if (currentHealth <= 0)
            {
                HandleDeath(source);
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
        
        [Server]
        private void HandleDeath(GameObject source)
        {

            var player = GetComponent<NetworkGamePlayer>();
            var sourcePlayer = source.GetComponent<NetworkGamePlayer>();

            player.Deaths += 1;
            sourcePlayer.Kills += 1;

            //Handle death on clients
            RpcHandleDeath(source.name);
            
            //do not respawn if the round is Over
            if(!GameManager.Instance.IsRoundOver) PlayerSpawnSystem.Instance.SpawnPlayer(gameObject, 
                PlayerSpawnSystem.Instance.GetSpawnPos(player.TeamID), MatchSettings.RespawnTime);

            StartCoroutine(CalculateDeathTimeReamining());
        }
        
        [ClientRpc]
        private void RpcHandleDeath(string source)
        {
            OnDeath?.Invoke(this, new DeathEventArgs { DeadObject = gameObject});
            GameManager.Instance.PlayerKilled(gameObject.name, source);
            
            DesactivateOnDeath();
            
            //Handle deathUI for the dead player
            if (deathUI == null)
            {
                Debug.LogWarning("DeathUI is missing in HealthSystem");
                return;
            }
            
            if (hasAuthority)
            {
                deathUI.gameObject.SetActive(true);
                deathUI.text = "YOU HAVE BEEN KILLED BY <i><color=#ff0000><b>" + source + "</b></color></i>";
            }
           
        }

        private void DesactivateOnDeath()
        {
            GetComponent<PlayerMovement>().CancelAllMovements();
            GetComponent<CapsuleCollider>().enabled = false;
            
            var playerInput = GetComponent<PlayerInput>();
            playerInput.currentActionMap = playerInput.actions.FindActionMap("DeadPlayer");
            
            foreach (GameObject obj in gameObjectsDesactivateOnDeath)
            {
                obj.SetActive(false);
            }
            
            foreach (Behaviour obj in behavioursDesactivateOnDeath)
            {
                obj.enabled = false;
            }
        }
        
        private IEnumerator CalculateDeathTimeReamining()
        {
            for (TimeRemainingBeforeRespawn = MatchSettings.RespawnTime; TimeRemainingBeforeRespawn > 0; TimeRemainingBeforeRespawn -= Time.deltaTime)
            {
                yield return null;
            }

            TimeRemainingBeforeRespawn = 0;
        }
        
        public void ReactivateOnRespawn()
        {
            ResetHealth();
            GetComponent<PlayerCam>().RestorePlayerCam();
            GetComponent<CapsuleCollider>().enabled = true;
            
            var playerInput = GetComponent<PlayerInput>();
            playerInput.currentActionMap = playerInput.actions.FindActionMap("Player");
            
            foreach (GameObject obj in gameObjectsDesactivateOnDeath)
            {
                obj.SetActive(true);
            }
            
            foreach (Behaviour obj in behavioursDesactivateOnDeath)
            {
                obj.enabled = true;
            }
            
            if(hasAuthority && deathUI != null) deathUI.gameObject.SetActive(false);
        }

        private void ClampHealth()
        {
            Mathf.Clamp(currentHealth, 0, maxHealth);
        }
    }
}
