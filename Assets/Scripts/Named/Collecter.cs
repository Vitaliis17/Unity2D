public class Collecter
{
    private int _coinAmount;

    public Collecter(int coinAmount = 0)
        => _coinAmount = coinAmount;

    public void Take(Coin coin)
        => _coinAmount += coin.GivePoints();
}