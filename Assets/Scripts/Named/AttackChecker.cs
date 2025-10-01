using UnityEngine;
using System;

[RequireComponent(typeof(CapsuleCollider2D))]
public class AttackChecker : MonoBehaviour
{
    [SerializeField, Min(0)] private int _maxEnemyAmount;
    [SerializeField] private LayerMask _layer;

    private CapsuleCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<CapsuleCollider2D>();
        _collider.isTrigger = true;
    }

    public Collider2D[] ReadEnemies()
        => Physics2D.OverlapCapsuleAll(_collider.transform.position, _collider.size, _collider.direction, 0f, _layer);
}