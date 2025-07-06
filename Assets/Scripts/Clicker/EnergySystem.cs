using System;
using System.Collections;
using UnityEngine;

public class EnergyModel
{
    public int MaxEnergy { get; private set; }
    public int CurrentEnergy { get; private set; }

    private int regenAmount;
    private int costEnergy;
    private float regenInterval;

    public event Action<int> OnEnergyChanged;

    public EnergyModel(ClickerConfig config)
    {
        MaxEnergy = config.maxEnergy;
        regenAmount = config.regenAmount;
        costEnergy = config.costEnergy;
        regenInterval = config.regenInterval;

        CurrentEnergy = MaxEnergy;
    }

    public IEnumerator RegenEnergy()
    {
        
        while (true)
        {
            yield return new WaitForSeconds(regenInterval);
            if (CurrentEnergy < MaxEnergy)
            {
                CurrentEnergy = Mathf.Min(CurrentEnergy + regenAmount, MaxEnergy);
                OnEnergyChanged?.Invoke(CurrentEnergy);
            }
        }
    }

    public bool TryConsume()
    {
        if (CurrentEnergy >= costEnergy)
        {
            CurrentEnergy -= costEnergy;
            OnEnergyChanged?.Invoke(CurrentEnergy);
            return true;
        }

        return false;
    }
}
