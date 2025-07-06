using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private TabView tabView;

    public override void InstallBindings()
    {
        Container.Bind<TabView>().FromInstance(tabView).AsSingle();
        Container.Bind<IServerQueue>().To<ServerQueue>().AsSingle();
        Container.Bind<TabPresenter>().AsSingle().NonLazy();
    }
}