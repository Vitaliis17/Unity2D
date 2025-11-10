using UnityEngine;

public class ItemTaker
{
    public void Take(Collecter collecter, Health health, params Collider2D[] colliders)
    {
        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out Coin coin))
                collecter.Take(coin);
            else if (collider.TryGetComponent(out Medkit medkit))
                health.Heal(medkit.GiveHealing());
        }
    }
}