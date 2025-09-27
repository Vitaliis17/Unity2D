using UnityEngine;

public class Jumper
{
    private readonly float _force;

    public Jumper(float force)
        => _force = force;

    public void Jump(Rigidbody2D rigidbody, float direction)
    {
        direction *= _force;
        rigidbody.velocity = new(rigidbody.velocity.x, direction);
    }
}