using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeatherView : MonoBehaviour
{
    private WeatherRequestService weatherRequestService;

    [SerializeField] private TextMeshProUGUI weatherText;
    [SerializeField] private Image weatherIcon;

    public void SetWeatherText(int degrees)
    {
        weatherText.text = $"Сегодня {degrees}F";
    }

    public void SetWeatherIcon(Sprite sprite)
    {
        weatherIcon.sprite = sprite;
    }
}

