[System.Serializable]
public class WeatherResponse
{
    public WeatherProperties properties;
}

[System.Serializable]
public class WeatherProperties
{
    public WeatherPeriod[] periods;
}

[System.Serializable]
public class WeatherPeriod
{
    public int temperature;
    public string icon;
}

