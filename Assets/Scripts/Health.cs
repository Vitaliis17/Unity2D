using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField, Min(0)] private float _maxHealth;

    private float _currentHealth;

    private void Awake()
        => _currentHealth = _maxHealth;

    public void TakeDamage(float damage)
    {
        if (damage <= 0)
            return;

        _currentHealth -= damage;

        if(IsDead())
            Destroy(gameObject);
    }

    public void Heal(float healingAmount)
    {
        if(healingAmount <= 0) 
            return;

        _currentHealth += healingAmount;
    }

    private bool IsDead()
        => _currentHealth <= 0;
}