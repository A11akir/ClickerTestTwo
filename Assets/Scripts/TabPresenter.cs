using UnityEngine;

public class TabPresenter
{
    private readonly TabView tabView;
    private readonly IServerQueue serverQueue;

    public TabPresenter(TabView tabView, IServerQueue serverQueue)
    {
        Debug.Log("TabPresenter");
        this.tabView = tabView;
        this.serverQueue = serverQueue;

        tabView.Initialize();
        tabView.OnTabSelected += OnTabSelected;

        tabView.ShowTab(TabType.Clicker);
    }

    private void OnTabSelected(TabType tab)
    {
        tabView.ShowTab(tab);
        serverQueue.CancelAll();

        switch (tab)
        {
            case TabType.Weather:
                serverQueue.EnqueueWeatherLoop();
                break;
            case TabType.Dogs:
                serverQueue.EnqueueBreedList();
                break;
        }
    }
}