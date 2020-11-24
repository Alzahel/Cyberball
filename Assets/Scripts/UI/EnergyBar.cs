using System;
using UnityEngine;

namespace UI
{
    public class EnergyBar : Utils.BarFill
    {
        [SerializeField] private EnergySystem energySystem;

        private void Update()
        {
            SetBarFillAmount(energySystem.GetEnergyPercent());            
        }
    }
}
