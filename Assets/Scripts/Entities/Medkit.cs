using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Medkit : MonoBehaviour
{
    [SerializeField, Min(0)] private int _healingHealthAmount;

    private void Awake()
        => GetComponent<Collider2D>().isTrigger = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Health health) == false)
            return;

        health.Heal(_healingHealthAmount);
        Destroy(gameObject);
    }
}