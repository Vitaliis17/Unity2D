using UnityEngine;
using System;

[Serializable]
public class Runner
{
    [SerializeField] private Rigidbody2D _rigidbody;
    
    [SerializeField, Min(0)] private float _speed;

    public void Move(float direction)
    {
        direction *= _speed * Time.fixedDeltaTime;
        _rigidbody.velocity = new(direction, _rigidbody.velocity.y);
    }

    public void MoveTowards(Transform target)
    {
        float nextPositionX = Mathf.MoveTowards(_rigidbody.position.x, target.position.x, _speed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(new(nextPositionX, _rigidbody.position.y));
    }
}