using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class WeatherRequestService
{
    private const string WeatherApiUrl = "https://api.weather.gov/gridpoints/TOP/32,81/forecast";

    public IEnumerator RequestWeather(Action<int, string> onSuccess)
    {
        using UnityWebRequest request = UnityWebRequest.Get(WeatherApiUrl);
        yield return request.SendWebRequest();

            string json = request.downloadHandler.text;
            var degreeStr = "61";
            var iconUrl = "https://example.com/weather.png"; // Аналогично

            onSuccess?.Invoke(int.Parse(degreeStr), iconUrl);
        
    }

    public IEnumerator LoadIcon(string url, Action<Sprite> onSuccess, Action<string> onError = null)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        Texture2D texture = DownloadHandlerTexture.GetContent(request);
        Sprite sprite = Sprite.Create(texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f));

        onSuccess?.Invoke(sprite);
    }
}

