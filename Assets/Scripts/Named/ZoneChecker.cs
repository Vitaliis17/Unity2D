using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class ZoneChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _layer;

    private CapsuleCollider2D _collider;

    private void Awake()
        => _collider = GetComponent<CapsuleCollider2D>();

    public Collider2D[] ReadColliders()
        => Physics2D.OverlapCapsuleAll(ReadPoint(), _collider.size, _collider.direction, 0f, _layer);

    public Collider2D ReadCollider()
        => Physics2D.OverlapCapsule(ReadPoint(), _collider.size, _collider.direction, 0f, _layer);

    private Vector2 ReadPoint()
        => (Vector2)_collider.transform.position + Vector2.up * _collider.offset.y + Vector2.right * _collider.offset.x * GetOffsetDirection();

    private int GetOffsetDirection()
    {
        const int NegativeDirection = -1;
        const int PositiveDirection = 1;

        const float Offset = 0.001f;

        int direction = transform.eulerAngles.y > -Offset && transform.eulerAngles.y < Offset ? PositiveDirection : NegativeDirection;
        return direction;
    }
}