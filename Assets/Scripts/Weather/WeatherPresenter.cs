using System.Collections;
using UnityEngine;

public class WeatherPresenter
{
    private WeatherModel model;
    private WeatherView view;
    private WeatherRequestService requestService;
    private MonoBehaviour coroutineRunner;

    private Coroutine requestCoroutine;
    private bool isTabActive = false;

    public WeatherPresenter(
        WeatherModel model,
        WeatherView view,
        WeatherRequestService requestService,
        MonoBehaviour coroutineRunner)
    {
        this.model = model;
        this.view = view;
        this.requestService = requestService;
        this.coroutineRunner = coroutineRunner;
    }


    public void OnTabOpened()
    {
        isTabActive = true;
        StartWeatherLoop();
    }

    public void OnTabClosed()
    {
        isTabActive = false;
        StopWeatherLoop();
    }

    private void StartWeatherLoop()
    {
        requestCoroutine = coroutineRunner.StartCoroutine(WeatherLoop());
    }

    private void StopWeatherLoop()
    {
        if (requestCoroutine != null)
        {
            coroutineRunner.StopCoroutine(requestCoroutine);
            requestCoroutine = null;
        }
    }
    
    private IEnumerator WeatherLoop()
    {
        while (isTabActive)
        {
            yield return requestService.RequestWeather(OnWeatherReceived);
            yield return new WaitForSeconds(5);
        }
    }

    private void OnWeatherReceived(int degrees, string iconUrl)
    {
        model.SetWeatherData(degrees, iconUrl);
        view.SetWeatherText(degrees);

        coroutineRunner.StartCoroutine(
            requestService.LoadIcon(iconUrl, sprite =>
            {
                view.SetWeatherIcon(sprite);
            }));
    }
}