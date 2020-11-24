using System;
using Mirror;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(EnergySystem))]
public class PlayerEnergy : NetworkBehaviour
{
    private PlayerMovement playerMovement;
    private EnergySystem energySystem;
    
    [SerializeField] private float maxEnergyAmount = 100;
    [SerializeField] private float energyRegenSpeed = 3f;
    [SerializeField] private float sprintDrainSpeed = 10f;
    
    //states
    private bool isSprinting;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        energySystem = GetComponent<EnergySystem>();
    }

    private void Start()
    {
        energySystem.MaxEnergy = maxEnergyAmount;
    }

    private void Update()
    {
        if(isSprinting) energySystem.DrainEnergyOverTime(sprintDrainSpeed);
        else energySystem.RegenEnergyOverTime(energyRegenSpeed);
        
        playerMovement.HasEnergy = energySystem.GetEnergyPercent() != 0;
    }

    public override void OnStartAuthority()
    {
        playerMovement.OnSprint += SprintDrain;
        energySystem.OnEnergyEmptied += EnergyEmptied;
    }
    
    public override void OnStopAuthority()
    {
        playerMovement.OnSprint -= SprintDrain;
        energySystem.OnEnergyEmptied -= EnergyEmptied;
    }

    private void SprintDrain(object sender, PlayerMovement.OnSprintEventArgs e)
    {
        isSprinting = e.IsSprinting;
    }
    
    private void EnergyEmptied(object sender, EventArgs e)
    {
        playerMovement.CancelAllSpecialMovements();
        playerMovement.HasEnergy = false;
    }
}