using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.VFX;
using DG.Tweening;

public class ClickerView : MonoBehaviour
{
    [SerializeField] private Button clickButton;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private VisualEffect clickEffect;
    [SerializeField] private RectTransform buttonTransform;
    [SerializeField] private Image energyBarImage;

    [SerializeField] private RectTransform coinFlyObject;
    [SerializeField] private RectTransform coinTargetTransform;
    [SerializeField] private RectTransform flyStartTransform;

    private float durationCoinsFly;
    private float moveAmountAnimateButtonClick;
    private float durationAnimateButtonClick;
    
    public event Action OnClick;

    public void Initialize(ClickerConfig config)
    {
        clickButton.onClick.AddListener(() => { AllClickEffect(); });

        durationCoinsFly = config.coinFlyDuration;
        moveAmountAnimateButtonClick = config.buttonClickMoveAmount;
        durationAnimateButtonClick = config.buttonClickDuration;
    }

    public void SetCoinText(int coins)
    {
        coinText.text = $"{coins}";
    }

    private void PlayClickEffect(bool isAuto = false)
    {
        Vector3 worldPos;

        if (isAuto)
        {
            worldPos = Vector3.zero;
        }
        else
        {
            Vector3 pos = Input.mousePosition;
            pos.z = 10f;
            worldPos = Camera.main.ScreenToWorldPoint(pos);
        }

        clickEffect.transform.position = worldPos;

        clickEffect.Stop();
        clickEffect.Reinit();
        clickEffect.SendEvent("PlayEffect");
        clickEffect.Play();
    }

    public void PlayCoinFlyAnimation()
    {
        coinFlyObject.gameObject.SetActive(true);

        coinFlyObject.anchoredPosition = flyStartTransform.anchoredPosition;

        Vector2 targetAnchoredPosition;
        RectTransform parentRect = coinFlyObject.parent as RectTransform;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentRect,
                RectTransformUtility.WorldToScreenPoint(null, coinTargetTransform.position),
                null,
                out targetAnchoredPosition))
        {
            coinFlyObject.DOAnchorPos(targetAnchoredPosition, durationCoinsFly)
                .SetEase(Ease.OutCubic)
                .OnComplete(() => { coinFlyObject.gameObject.SetActive(false); });
        }
    }

    private void AnimateButtonClick()
    {
        buttonTransform.DOKill();
        buttonTransform.localPosition = Vector3.zero;

        buttonTransform.DOAnchorPosY(-moveAmountAnimateButtonClick, durationAnimateButtonClick)
            .SetRelative()
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                buttonTransform.DOAnchorPosY(moveAmountAnimateButtonClick, durationAnimateButtonClick)
                    .SetRelative()
                    .SetEase(Ease.InQuad);
            });
    }


    public void SetEnergyBar(int energy, int maxEnergy)
    {
        energyBarImage.fillAmount = (float)energy / maxEnergy;
    }

    public void AllClickEffect(bool isAuto = false)
    {
        PlayClickEffect(isAuto);
        PlayCoinFlyAnimation();
        AnimateButtonClick();
        OnClick?.Invoke();
    }
}