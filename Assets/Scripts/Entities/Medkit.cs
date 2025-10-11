using UnityEngine;
using System;

[RequireComponent(typeof(Collider2D))]
public class Medkit : MonoBehaviour
{
    [SerializeField, Min(0)] private int _healingHealthAmount;

    public event Action<Medkit> Releasing;

    private void Awake()
        => GetComponent<Collider2D>().isTrigger = true;

    public int GiveHealing()
    {
        Releasing?.Invoke(this);

        return _healingHealthAmount;
    }
}