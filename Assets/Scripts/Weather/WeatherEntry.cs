using UnityEngine;

public class WeatherEntry : MonoBehaviour
{
    private WeatherPresenter weatherPresenter;

    [SerializeField] private WeatherView weatherView;

    private void Awake()
    {
        var model = new WeatherModel();
        var requestService = new WeatherRequestService();
        weatherPresenter = new WeatherPresenter(model, weatherView, requestService, this);
    }

    private void OnEnable()
    {
        weatherPresenter.OnTabOpened();
    }

    private void OnDisable()
    {
        weatherPresenter.OnTabClosed();
    }
}
