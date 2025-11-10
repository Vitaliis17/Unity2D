using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    private const int _minValue = 0;

    [SerializeField, Min(_minValue)] private int _maxValue;
    
    private float _ñurrentValue;

    public event Action<int> MaxValueChanged;
    public event Action<float> CurrentValueChanged;

    public event Action Died;

    private void Start()
    {
        MaxValueChanged?.Invoke(_maxValue);

        SetCurrentValue(_maxValue);
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            return;

        float nextValue = Mathf.Clamp(_ñurrentValue - damage, _minValue, _maxValue);
        SetCurrentValue(nextValue);

        if (IsAlive() == false)
            Died?.Invoke();
    }

    public void Heal(int healingAmount)
    {
        if (healingAmount <= 0)
            return;

        float nextValue = Mathf.Clamp(_ñurrentValue + healingAmount, _minValue, _maxValue);
        SetCurrentValue(nextValue);
    }

    public float Transfer(int value)
    {
        float transferirngValue = _ñurrentValue < value ? _ñurrentValue : value;

        TakeDamage(value);

        return transferirngValue;
    }

    private bool IsAlive()
        => _ñurrentValue > _minValue;

    private void SetCurrentValue(float value)
    {
        _ñurrentValue = value;
        CurrentValueChanged?.Invoke(_ñurrentValue);
    }
}