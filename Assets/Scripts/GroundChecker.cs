using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Collider2D))]
public class GroundChecker : MonoBehaviour
{
    public bool IsGrounded {  get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        const bool IsTrigger = true;

        if (collision.TryGetComponent(out TilemapCollider2D _))
            IsGrounded = IsTrigger;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        const bool NoTrigger = false;

        if (collision.TryGetComponent(out TilemapCollider2D _))
            IsGrounded = NoTrigger;
    }
}