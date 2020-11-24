using System;
using Health;
using UnityEngine;

namespace UI
{
    public class HealthBar : Utils.BarFill
    {
        [SerializeField] private HealthSystem health;

        [SerializeField] private bool isHorizontal; 

        private void Update()
        {
            if(isHorizontal) SetBarFillAmount(xAmount:health.GetHealthPercent());
            else SetBarFillAmount(health.GetHealthPercent());            
        }
    }
}
