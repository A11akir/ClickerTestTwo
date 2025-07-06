using UnityEngine;
using Zenject;

public class ClickerInstaller : MonoInstaller
{
    [SerializeField] private ClickerConfig config;
    
    public override void InstallBindings()
    {
        Container.Bind<ClickerConfig>().FromInstance(config).AsSingle();
        
        Container.Bind<ClickerView>().FromComponentInHierarchy().AsSingle();
        Container.Bind<SoundPlayer>().FromComponentInHierarchy().AsSingle();
        
        Container.Bind<EnergyModel>().AsSingle();
        Container.Bind<CurrencyModel>().AsSingle();
        Container.Bind<AutoGrind>().AsSingle();
        Container.Bind<ClickerPresenter>().AsSingle();
    }
}