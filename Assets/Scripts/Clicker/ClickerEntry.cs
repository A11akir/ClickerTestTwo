using UnityEngine;
using Zenject;

public class ClickerEntry : MonoBehaviour
{
    private AutoGrind autoGrind;
    private EnergyModel energyModel;
    private ClickerView clickerView;
    private ClickerConfig clickerConfig;

    [Inject]
    public void Construct(
        AutoGrind autoGrind,
        EnergyModel energyModel,
        ClickerView clickerView,
        ClickerPresenter presenter,
        ClickerConfig clickerConfig
    )
    {
        this.autoGrind = autoGrind;
        this.energyModel = energyModel;
        this.clickerView = clickerView;
        this.clickerConfig = clickerConfig;
    }

    private void Start()
    {
        clickerView.Initialize(clickerConfig);
        
        energyModel.OnEnergyChanged += (currentEnergy) =>
        {
            clickerView.SetEnergyBar(currentEnergy, energyModel.MaxEnergy);
        };
        clickerView.SetEnergyBar(energyModel.CurrentEnergy, energyModel.MaxEnergy);

        StartCoroutine(energyModel.RegenEnergy());
        StartCoroutine(autoGrind.AutoGrindCoroutine());
    }
}

