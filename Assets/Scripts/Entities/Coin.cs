using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Coin : MonoBehaviour
{
    private CircleCollider2D _circleCollider;

    private void Awake()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
        _circleCollider.isTrigger = true;
    }
}
