using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class DogBreedRequestService
{
    private UnityWebRequest currentRequest;
    private string lastUrl;
    public string GetCurrentUrl() => lastUrl;

    private const string BreedsUrl = "https://api.thedogapi.com/v1/breeds";
    private const string BreedDetailUrlTemplate = "https://api.dogapi.dog/api/v2/breeds/{0}";

    public void CancelRequest()
    {
        if (currentRequest != null && !currentRequest.isDone)
        {
            currentRequest.Abort();
        }
    }

    public IEnumerator RequestBreeds(Action<List<DogBreed>> onSuccess, Action<string> onError)
    {
        lastUrl = BreedsUrl;
        currentRequest = UnityWebRequest.Get(lastUrl);
        currentRequest.SetRequestHeader("Accept", "application/json");

        yield return currentRequest.SendWebRequest();

        if (currentRequest.result != UnityWebRequest.Result.Success)
        {
            onError?.Invoke(currentRequest.error);
            yield break;
        }

        try
        {
            string json = currentRequest.downloadHandler.text;
            Debug.Log("🐾 Raw JSON:\n" + json);

            var breeds = JsonConvert.DeserializeObject<List<DogBreed>>(json);

            if (breeds == null)
            {
                onError?.Invoke("🐛 JSON parse error: breeds == null");
                yield break;
            }

            onSuccess?.Invoke(breeds);
        }
        catch (Exception e)
        {
            onError?.Invoke("🐛 JSON parse error: " + e.Message);
        }
    }

    public IEnumerator RequestBreedFact(int id, Action<DogFact> onSuccess, Action<string> onError)
    {
        lastUrl = string.Format(BreedDetailUrlTemplate, id);
        currentRequest = UnityWebRequest.Get(lastUrl);
        currentRequest.SetRequestHeader("Accept", "application/json");

        yield return currentRequest.SendWebRequest();

        if (currentRequest.result != UnityWebRequest.Result.Success)
        {
            onError?.Invoke(currentRequest.error);
            yield break;
        }

        try
        {
            string json = currentRequest.downloadHandler.text;
            Debug.Log("🐕 Raw breed fact JSON: " + json);

            var response = JsonUtility.FromJson<DogFactResponse>(json);

            if (response == null || response.data == null)
            {
                onError?.Invoke("🐛 JSON parse error: Response or data is null");
                yield break;
            }

            onSuccess?.Invoke(response.data);
        }
        catch (Exception e)
        {
            onError?.Invoke("🐛 JSON parse error: " + e.Message);
        }
    }

    [Serializable]
    private class DogFactResponse { public DogFact data; }
}


