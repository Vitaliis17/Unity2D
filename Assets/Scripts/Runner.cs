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

    public void MoveTowards(Rigidbody2D rigidbody, Transform target)
    {
        float nextPositionX = Mathf.MoveTowards(rigidbody.position.x, target.position.x, _speed * Time.fixedDeltaTime);
        rigidbody.MovePosition(new(nextPositionX, rigidbody.position.y));
    }
}