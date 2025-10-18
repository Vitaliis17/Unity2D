using UnityEngine;

[RequireComponent(typeof(Health), typeof(Collecter))]
public class ItemTaker : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Collecter _collecter;

    public void Take(params Collider2D[] colliders)
    {
        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out Coin coin))
                _collecter.Take(coin);
            else if (collider.TryGetComponent(out Medkit medkit))
                _health.Heal(medkit.GiveHealing());
        }
    }
}