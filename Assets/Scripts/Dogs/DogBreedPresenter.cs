using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBreedPresenter
{
    private readonly DogBreedModel model;
    private readonly DogBreedView view;
    private readonly DogBreedRequestService service;
    private readonly MonoBehaviour coroutineRunner;

    private Coroutine currentCoroutine;

    public DogBreedPresenter(DogBreedModel model, DogBreedView view, DogBreedRequestService service, MonoBehaviour coroutineRunner)
    {
        this.model = model;
        this.view = view;
        this.service = service;
        this.coroutineRunner = coroutineRunner;

        view.OnBreedClicked += OnBreedClicked;
    }

    public void OnTabOpened()
    {
        Debug.Log("OnTabOpened");
        view.HidePopup();
        view.ShowLoading(true);
        currentCoroutine = coroutineRunner.StartCoroutine(
            service.RequestBreeds(OnBreedsReceived, OnError));
    }

    public void OnTabClosed()
    {
        if (currentCoroutine != null)
        {
            service.CancelRequest();
            coroutineRunner.StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }

        view.ShowLoading(false);
        view.HidePopup();
    }

    private void OnBreedsReceived(List<DogBreed> breeds)
    {
        model.Breeds = breeds;
        view.ShowBreedList(breeds);
        view.ShowLoading(false);
    }

    private void OnBreedClicked(int id)
    {
        if (currentCoroutine != null)
        {
            service.CancelRequest();
            coroutineRunner.StopCoroutine(currentCoroutine);
        }

        view.HidePopup();
        view.ShowLoading(true);

        currentCoroutine = coroutineRunner.StartCoroutine(
            service.RequestBreedFact(id, OnFactReceived, OnError));
    }

    private void OnFactReceived(DogFact fact)
    {
        model.CurrentFact = fact;
        view.ShowLoading(false);
        view.ShowPopup(fact.name, fact.description);
    }

    private void OnError(string error)
    {
        Debug.LogError("Ошибка собак: " + (error ?? "null"));

        if (service != null)
        {
            Debug.LogError("URL: " + service.GetCurrentUrl());
        }

        view.ShowLoading(false);
    }
}

