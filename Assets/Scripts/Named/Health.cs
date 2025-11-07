using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    private const int _minValue = 0;

    [SerializeField, Min(_minValue)] private int _maxValue;

    private int _currentValue;

    public event Action<int> MaxValueChanged;
    public event Action<int> CurrentValueChanged;

    public event Action Died;

    private void Start()
    {
        _currentValue = _maxValue;

        MaxValueChanged?.Invoke(_maxValue);
        CurrentValueChanged?.Invoke(_currentValue);
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            return;

        _currentValue = Mathf.Clamp(_currentValue - damage, _minValue, _maxValue);

        CurrentValueChanged?.Invoke(_currentValue);

        if (IsDead())
            Died?.Invoke();
    }

    public void Heal(int healingAmount)
    {
        if (healingAmount <= 0)
            return;

        _currentValue = Mathf.Clamp(_currentValue + healingAmount, _minValue, _maxValue);

        CurrentValueChanged?.Invoke(_currentValue);
    }

    private bool IsDead()
        => _currentValue <= _minValue;
}