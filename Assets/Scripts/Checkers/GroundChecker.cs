using UnityEngine;
using System;

[RequireComponent(typeof(CapsuleCollider2D))]
public class GroundChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _layer;

    private CapsuleCollider2D _collider;

    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        _collider = GetComponent<CapsuleCollider2D>();
        _collider.isTrigger = true;
    }

    private void FixedUpdate()
        => IsGrounded = IsTriggered();

    public bool IsTriggered()
        => Physics2D.OverlapCapsule((Vector2)_collider.transform.position + _collider.offset, _collider.size, _collider.direction, 0f, _layer);
}