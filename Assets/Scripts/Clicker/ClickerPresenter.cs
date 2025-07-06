
using UnityEngine;

public class ClickerPresenter
{
    private CurrencyModel currencyModel;
    private ClickerView clickerView;
    private EnergyModel energyModel;
    private SoundPlayer soundPlayer;

    public ClickerPresenter(CurrencyModel currencyModel, ClickerView clickerView, EnergyModel energyModel, SoundPlayer soundPlayer)
    {
        this.currencyModel = currencyModel;
        this.clickerView = clickerView;
        this.energyModel = energyModel;
        this.soundPlayer = soundPlayer;

        clickerView.OnClick += HandleClick;

        UpdateView();
    }

    private void HandleClick()
    {
        if (energyModel.TryConsume())
        {
            currencyModel.AddCoins();
            soundPlayer.PlayCoinSound();
            UpdateView();
        }
    }

    private void UpdateView()
    {
        clickerView.SetCoinText(currencyModel.Coins);
    }
}
