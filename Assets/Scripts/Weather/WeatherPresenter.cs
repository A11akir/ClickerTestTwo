using System.Collections;
using UnityEngine;

public class WeatherPresenter
{
    private readonly WeatherModel model;
    private readonly WeatherView view;
    private readonly WeatherRequestService requestService;
    private readonly MonoBehaviour coroutineRunner;

    private Coroutine requestCoroutine;
    private bool isTabActive;

    private const int TimeDelayRequest = 5;

    public WeatherPresenter(WeatherModel model, WeatherView view, WeatherRequestService requestService, MonoBehaviour coroutineRunner)
    {
        this.model = model;
        this.view = view;
        this.requestService = requestService;
        this.coroutineRunner = coroutineRunner;
    }

    public void OnTabOpened()
    {
        isTabActive = true;
        requestCoroutine = coroutineRunner.StartCoroutine(WeatherLoop());
    }

    public void OnTabClosed()
    {
        isTabActive = false;
        if (requestCoroutine != null)
        {
            requestService.CancelRequest();
            coroutineRunner.StopCoroutine(requestCoroutine);
            requestCoroutine = null;
        }
    }

    private IEnumerator WeatherLoop()
    {
        while (isTabActive)
        {
            yield return requestService.RequestWeather(() => isTabActive, OnWeatherReceived, OnWeatherError);
            yield return new WaitForSeconds(TimeDelayRequest);
        }
    }

    private void OnWeatherReceived(int degrees, string iconUrl)
    {
        model.SetWeatherData(degrees, iconUrl);
        view.SetWeatherText(degrees);
        coroutineRunner.StartCoroutine(requestService.LoadIcon(iconUrl, view.SetWeatherIcon));
    }

    private void OnWeatherError(string error) => Debug.LogError("Ошибка погоды: " + error);
}