using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;

public class DogBreedView : MonoBehaviour
{
    public event Action<int> OnBreedClicked;

    [SerializeField] private Transform breedListRoot;
    [SerializeField] private GameObject breedItemPrefab;
    [SerializeField] private GameObject loadingSpinner;
    [SerializeField] private GameObject popup;
    [SerializeField] private TextMeshProUGUI popupTitle;
    [SerializeField] private TextMeshProUGUI popupDescription;

    public void ShowLoading(bool isLoading)
    {
        loadingSpinner.SetActive(isLoading);
    }

    public void ClearBreedList()
    {
        foreach (Transform child in breedListRoot)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void ShowBreedList(List<DogBreed> breeds)
    {
        ClearBreedList();

        for (int i = 0; i < Mathf.Min(10, breeds.Count); i++)
        {
            var breed = breeds[i];
            var go = Instantiate(breedItemPrefab, breedListRoot);
            var text = go.GetComponentInChildren<Text>();
            text.text = $"{i + 1}. {breed.name}";

            var button = go.GetComponent<Button>();
            int breedId = breed.id;
            button.onClick.AddListener(() => OnBreedClicked?.Invoke(breedId));
        }
    }

    public void ShowPopup(string title, string description)
    {
        popupTitle.text = title;
        popupDescription.text = description;
        popup.SetActive(true);
    }

    public void HidePopup()
    {
        popup.SetActive(false);
    }
}