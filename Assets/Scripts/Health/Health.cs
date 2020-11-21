using Mirror;
using System;
using UnityEngine;

public class Health : NetworkBehaviour
{
    [SerializeField] private int maxHealth = 100;

    [SyncVar(hook = nameof(HandleHealthChanged))]
    private int health = 0;

    //public static event EventHandler<DeathEventArgs> OnDeath;
    //public event EventHandler<HealthChangedEventArgs> OnHealthchanged;

    public bool IsDead => health == 0;

    public override void OnStartServer()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        health = maxHealth;
    }

    [ServerCallback]

    private void OnDestroy()
    {
        //OnDeath?.Invoke(this, new DeathEventArgs { ConnectionToClient = connectionToClient });
    }

    [Server]
    public void Add(int value)
    {
        value = Mathf.Max(value, 0);

        health = Mathf.Min(health + value, maxHealth);
    }
    
    [Server]
    public void Remove(int value)
    {
        value = Mathf.Max(value, 0);

        health = Mathf.Max(health - value, 0);

        if (health <= 0)
        {
            //OnDeath?.Invoke(this, new DeathEventArgs { ConnectionToClient = connectionToClient });

            RPCHandleDeath();
        } 
    }

    private void HandleHealthChanged(int oldValue, int newValue)
    {
        /*OnHealthchanged?.Invoke(this, new HealthChangedEventArgs
        {
            Health = health,
            MaxHealth = maxHealth
        });*/
    }

    [ClientRpc]
    private void RPCHandleDeath()
    {
    gameObject.SetActive(false);
    }
}
