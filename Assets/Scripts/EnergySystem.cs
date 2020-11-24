using System;
using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    //Energy variables
    public float MaxEnergy { get; set; } = 100;
    
    private float currentEnergy;

    public event EventHandler OnEnergyEmptied;
    public event EventHandler<OnEnergyChangedEventArgs> OnEnergyChanged;
    
    public class OnEnergyChangedEventArgs : EventArgs
    {
        public float CurrentEnergy;
    }
    private void Start()
    {
        currentEnergy = MaxEnergy;
    }

    private void Update()
    {
        if(currentEnergy <= 0) EnergyEmptied();
    }

    private void ClampEnergy()
    {
        //Makes sure energyAmount can't go out the boundaries of 0 / max Energy
        currentEnergy = Mathf.Clamp(currentEnergy, 0, MaxEnergy);
    }
    
    public float GetEnergyPercent()
    {
        return Mathf.Clamp01(currentEnergy / MaxEnergy);
    }
    
    public void DrainEnergy(float drainAmount)
    {
        currentEnergy -= drainAmount;
        ClampEnergy();
        
        OnEnergyChanged?.Invoke(this, new OnEnergyChangedEventArgs{CurrentEnergy = currentEnergy});
    }

    public void DrainEnergyOverTime(float drainSpeed)
    {
        currentEnergy -= drainSpeed * Time.deltaTime;
        ClampEnergy();
        
        OnEnergyChanged?.Invoke(this, new OnEnergyChangedEventArgs{CurrentEnergy = currentEnergy});
    }

    public void RegenEnergy(float regenAmount)
    {
        currentEnergy += regenAmount;
        ClampEnergy();
        
        OnEnergyChanged?.Invoke(this, new OnEnergyChangedEventArgs{CurrentEnergy = currentEnergy});
    }

    public void RegenEnergyOverTime(float regenSpeed)
    {
        currentEnergy += regenSpeed * Time.deltaTime;
        ClampEnergy();
        
        OnEnergyChanged?.Invoke(this, new OnEnergyChangedEventArgs{CurrentEnergy = currentEnergy});
    }

    public void EnergyEmptied()
    {
        OnEnergyEmptied?.Invoke(this, EventArgs.Empty);
    }
}