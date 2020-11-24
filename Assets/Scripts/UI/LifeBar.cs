using System;
using Health;
using UnityEngine;

namespace UI
{
    public class LifeBar : Utils.BarFill
    {
        [SerializeField] private HealthSystem health;

        private void Update()
        {
            SetBarFillAmount(health.GetHealthPercent());            
        }
    }
}
