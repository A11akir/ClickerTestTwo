using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class WeatherRequestService
{ 
    private const string WeatherApiUrl = "https://api.weather.gov/gridpoints/TOP/32,81/forecast";

    private UnityWebRequest currentRequest;

    public void CancelRequest()
    {
        if (currentRequest == null) return;

        if (currentRequest.isDone) return;
        
        currentRequest.Abort();
    }
    
    public IEnumerator RequestWeather(
        Func<bool> isTabStillActive,
        Action<int, string> onSuccess,
        Action<string> onError = null)
    {
        if (!isTabStillActive()) yield break;

        currentRequest = UnityWebRequest.Get(WeatherApiUrl);

        yield return currentRequest.SendWebRequest();

        if (!isTabStillActive()) yield break;
        
        if (currentRequest.result != UnityWebRequest.Result.Success)
        {
            onError?.Invoke(currentRequest.error);
            yield break;
        }

        try
        {
            string json = currentRequest.downloadHandler.text;
            var data = JsonUtility.FromJson<WeatherResponse>(json);

            var firstPeriod = data.properties.periods[0];
            int temp = firstPeriod.temperature;
            string iconUrl = firstPeriod.icon;

            onSuccess?.Invoke(temp, iconUrl);
        }
        catch (Exception e)
        {
            onError?.Invoke("Ошибка парсинга данных");
        }
    }

    public IEnumerator LoadIcon(string url, Action<Sprite> onSuccess, Action<string> onError = null)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            onError?.Invoke(request.error);

            yield break;
        }

        Texture2D texture = DownloadHandlerTexture.GetContent(request);
        Sprite sprite = Sprite.Create(texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f));

        onSuccess?.Invoke(sprite);
    }
}