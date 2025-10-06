using UnityEngine;

public class Health : MonoBehaviour
{
    private const int _minValue = 0;

    [SerializeField, Min(_minValue)] private int _maxValue;

    private int _currentValue;

    private void Awake()
        => _currentValue = _maxValue;

    public void TakeDamage(int damage)
    {
        const int DamageCoefficient = -1;

        if (damage <= 0)
            return;

        TakeHealing(damage * DamageCoefficient);

        if(IsDead())
            Destroy(gameObject);
    }

    public void Heal(int healingAmount)
    {
        if(healingAmount <= 0) 
            return;

        TakeHealing(healingAmount);
    }

    private void TakeHealing(int addingValue)
        => _currentValue = Mathf.Clamp(_currentValue + addingValue, _minValue, _maxValue);

    private bool IsDead()
        => _currentValue <= _minValue;
}