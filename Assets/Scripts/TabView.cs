using System;
using UnityEngine;
using UnityEngine.UI;

public class TabView : MonoBehaviour
{
    public event Action<TabType> OnTabSelected;

    [SerializeField] private GameObject clickerTab;
    [SerializeField] private GameObject weatherTab;
    [SerializeField] private GameObject dogsTab;

    [SerializeField] private Button clickerButton;
    [SerializeField] private Button weatherButton;
    [SerializeField] private Button dogsButton;

    public void Initialize()
    {
        clickerButton.onClick.AddListener(() => OnTabSelected?.Invoke(TabType.Clicker));
        weatherButton.onClick.AddListener(() => OnTabSelected?.Invoke(TabType.Weather));
        dogsButton.onClick.AddListener(() => OnTabSelected?.Invoke(TabType.Dogs));
    }

    public void ShowTab(TabType type)
    {
        clickerTab?.SetActive(false);
        weatherTab?.SetActive(false);
        dogsTab?.SetActive(false);

        switch (type)
        {
            case TabType.Clicker:
                clickerTab?.SetActive(true);
                break;
            case TabType.Weather:
                weatherTab?.SetActive(true);
                break;
            case TabType.Dogs:
                dogsTab?.SetActive(true);
                break;
        }
    }
}