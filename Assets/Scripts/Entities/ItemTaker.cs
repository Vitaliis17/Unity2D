using UnityEngine;

public class ItemTaker : MonoBehaviour
{
    public void Take(params Collider2D[] colliders)
    {
        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out Coin coin) && TryGetComponent(out Collecter collecter))
                collecter.Take(coin);
            else if (collider.TryGetComponent(out Medkit medkit) && TryGetComponent(out Health health))
                health.Heal(medkit.HealingHealthAmount);

            Destroy(collider.gameObject);
        }
    }
}