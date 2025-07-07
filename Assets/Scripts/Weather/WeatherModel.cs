public class WeatherModel
{
    public int Degrees { get; private set; }
    public string IconUrl { get; private set; }

    public void SetWeatherData(int degrees, string iconUrl)
    {
        Degrees = degrees;
        IconUrl = iconUrl;
    }
}

