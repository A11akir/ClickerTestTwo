using UnityEngine;

public class DogBreedEntry : MonoBehaviour
{
    [SerializeField] private DogBreedView view;

    private DogBreedPresenter dogBreedPresenter;
    private DogBreedModel model;
    private DogBreedRequestService service;

    private void Awake()
    {
        model = new DogBreedModel();
        service = new DogBreedRequestService();

        dogBreedPresenter = new DogBreedPresenter(model, view, service, this);
    }

    private void OnEnable()
    {
        dogBreedPresenter.OnTabOpened();
    }

    private void OnDisable()
    {
        dogBreedPresenter.OnTabClosed();
    }
}

