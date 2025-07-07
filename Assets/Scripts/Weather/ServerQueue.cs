using UnityEngine;

public class ServerQueue : IServerQueue
{
    public void CancelAll()
    {
        Debug.Log("❌ Все запросы отменены");
    }

    public void EnqueueWeatherLoop()
    {
        Debug.Log("🌦️ Запущен цикл запроса погоды");
    }

    public void EnqueueBreedList()
    {
        Debug.Log("🐶 Отправлен запрос на список пород");
    }
}