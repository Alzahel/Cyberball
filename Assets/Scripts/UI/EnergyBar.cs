using UnityEngine;
using Utils;

namespace UI
{
    public class EnergyBar : BarFill
    {
        [SerializeField] private EnergySystem energySystem;

        private void Update()
        {
            SetBarFillAmount(energySystem.GetEnergyPercent());
        }
    }
}