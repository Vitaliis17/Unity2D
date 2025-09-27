using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class CoinTaker : MonoBehaviour
{
    private int _coinAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
            Take(coin);
    }

    private void Take(Coin coin)
    {
        _coinAmount++;
        Destroy(coin.gameObject);
    }
}