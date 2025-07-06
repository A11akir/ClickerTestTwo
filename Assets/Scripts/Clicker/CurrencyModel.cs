
public class CurrencyModel
{
    public int Coins { get; private set; }
    private int coinsPerClick;

    public CurrencyModel(ClickerConfig config)
    {
        coinsPerClick = config.coinsPerClick;
    }

    public void AddCoins()
    {
        Coins += coinsPerClick;
    }
}
