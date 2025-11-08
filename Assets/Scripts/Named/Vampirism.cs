public class Vampirism
{
    public void Transfer(Health taker, Health giver, int healthAmount)
    {
        giver.TakeDamage(healthAmount);
        taker.Heal(healthAmount);
    }
}