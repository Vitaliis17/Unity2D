using UnityEngine;
using System;

[RequireComponent(typeof(CapsuleCollider2D))]
public class AttackChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _layer;

    private CapsuleCollider2D _collider;

    public event Action<Player> OnPlayerTriggered;

    private void Awake()
    {
        _collider = GetComponent<CapsuleCollider2D>();
        _collider.isTrigger = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            OnPlayerTriggered?.Invoke(player);
    }

    public Collider2D[] ReadEnemies()
        => Physics2D.OverlapCapsuleAll(_collider.transform.position, _collider.size, _collider.direction, 0f, _layer);

}