using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TriggerHandler : MonoBehaviour
{
    public event Action<bool> Triggered;

    private Collider2D Collider;

    private void Awake()
        => Collider = GetComponent<Collider2D>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        const bool IsTrigger = true;

        Triggered?.Invoke(IsTrigger);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        const bool NoTrigger = false;

        Triggered?.Invoke(NoTrigger);
    }
}