using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Medkit : MonoBehaviour
{
    [field: SerializeField, Min(0)] public int HealingHealthAmount { get; private set; }

    private void Awake()
        => GetComponent<Collider2D>().isTrigger = true;
}