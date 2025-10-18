using UnityEngine;

public class Runner : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _speed;

    public void Move(Rigidbody2D rigidbody, float direction)
    {
        direction *= _speed * Time.fixedDeltaTime;
        rigidbody.velocity = new(direction, rigidbody.velocity.y);
    }
}