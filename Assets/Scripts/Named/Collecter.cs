using UnityEngine;

public class Collecter : MonoBehaviour
{
    private int _coinAmount;

    private void Awake()
        => _coinAmount = 0;

    public void Take(Coin coin)
        => _coinAmount += coin.GivePoints();
}