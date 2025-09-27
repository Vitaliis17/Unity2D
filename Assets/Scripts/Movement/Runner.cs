using UnityEngine;

public class Runner
{
    private readonly float _speed;

    public Runner(float speed)
        => _speed = speed;

    public void Move(Rigidbody2D rigidbody, float direction)
    {
        direction *= _speed * Time.fixedDeltaTime;
        rigidbody.velocity = new(direction, rigidbody.velocity.y);
    }
}