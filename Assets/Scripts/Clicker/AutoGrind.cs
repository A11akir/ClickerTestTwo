using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGrind
{
    private ClickerView clickerView;
    private EnergyModel energyModel;
    private float grindInterval;

    public AutoGrind(ClickerView view, EnergyModel energy, ClickerConfig config)
    {
        clickerView = view;
        energyModel = energy;
        grindInterval = config.autoGrindInterval;
    }

    public IEnumerator AutoGrindCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(grindInterval);
            if (energyModel.TryConsume())
            {
                clickerView.AllClickEffect(true);
            }
        }
    }
}
