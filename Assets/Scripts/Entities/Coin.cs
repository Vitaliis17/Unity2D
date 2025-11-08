using UnityEngine;
using System;

[RequireComponent(typeof(Collider2D), typeof(Animator))]
public class Coin : MonoBehaviour
{
    [SerializeField, Min(0)] private int _pointAmount;

    private Collider2D _collider;

    public event Action<Coin> Releasing;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
    }

    public int GivePoints()
    {
        Releasing?.Invoke(this);

        return _pointAmount;
    }
}