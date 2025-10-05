using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField, Min(0)] private int _maxHealthAmount;

    private int _currentHealthAmount;

    private void Awake()
        => _currentHealthAmount = _maxHealthAmount;

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            return;

        _currentHealthAmount -= damage;

        if(IsDead())
            Destroy(gameObject);
    }

    public void Heal(int healingAmount)
    {
        if(healingAmount <= 0) 
            return;

        int nextHealthAmount = _currentHealthAmount + healingAmount;

        if(nextHealthAmount > _maxHealthAmount)
            nextHealthAmount = _maxHealthAmount;

        _currentHealthAmount = nextHealthAmount;
    }

    private bool IsDead()
        => _currentHealthAmount <= 0;
}