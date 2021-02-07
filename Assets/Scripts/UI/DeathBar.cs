using System;
using Cyberball;
using Health;
using UnityEngine;

namespace UI
{
    public class DeathBar : Utils.BarFill
    {
        [SerializeField] private HealthSystem health;

        [SerializeField] private bool isHorizontal; 

        private void Update()
        {
            if(isHorizontal) SetBarFillAmount(xAmount:Mathf.Clamp01(health.TimeRemainingBeforeRespawn / MatchSettings.RespawnTime));
            else SetBarFillAmount(Mathf.Clamp01(health.TimeRemainingBeforeRespawn / MatchSettings.RespawnTime));            
        }
    }
}
