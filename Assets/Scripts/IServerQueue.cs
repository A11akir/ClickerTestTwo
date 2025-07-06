public interface IServerQueue
{
    void CancelAll();
    void EnqueueWeatherLoop();
    void EnqueueBreedList();
}