public class Vampirism
{
    public void Transfer(Health taker, Health giver, int healthAmount)
    {
        int health = (int)giver.Transfer(healthAmount);
        taker.Heal(health);
    }
}