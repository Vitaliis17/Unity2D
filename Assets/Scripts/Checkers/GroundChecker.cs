using UnityEngine;
using UnityEngine.Tilemaps;
using System;

[RequireComponent(typeof(CapsuleCollider2D))]
public class GroundChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _layer;

    private CapsuleCollider2D _collider;
    private int _triggerAmount;

    public event Action TriggerEntered;
    public event Action TriggerExited;

    private void Awake()
    {
        _collider = GetComponent<CapsuleCollider2D>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out TilemapCollider2D _) == false)
            return;

        if (IsNoTrigger())
            TriggerEntered?.Invoke();

        _triggerAmount++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out TilemapCollider2D _) == false)
            return;

        _triggerAmount--;

        if (IsNoTrigger())
            TriggerExited?.Invoke();
    }

    public bool IsTriggered()
        => Physics2D.OverlapCapsule(_collider.transform.position, _collider.size, _collider.direction, 0f, _layer);

    private bool IsNoTrigger()
        => _triggerAmount == 0;
}