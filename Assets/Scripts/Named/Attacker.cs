using UnityEngine;

public class Attacker 
{
    private readonly int _damage;

    public Attacker(int damage)
        => _damage = damage;

    public void Attack(Health health)
        => health.TakeDamage(_damage);
}