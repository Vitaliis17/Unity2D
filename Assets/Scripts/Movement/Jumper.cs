using UnityEngine;

public class Jumper
{
    private readonly float _force;

    public Jumper(float force)
    {
        _force = force;
        StopJumping();
    }

    public bool IsJumping { get; private set; }

    public void Jump(Rigidbody2D rigidbody, float direction)
    {
        direction *= _force;
        rigidbody.velocity = new(rigidbody.velocity.x, direction);

        IsJumping = true;
    }

    public void StopJumping()
        => IsJumping = false;
}