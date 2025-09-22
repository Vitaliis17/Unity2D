using UnityEngine;
using System;

[Serializable]
public class Jumper
{
    [SerializeField] private Rigidbody2D _rigidbody;
    
    [SerializeField, Min(0)] private float _force;

    public void Jump(float direction)
    {
        direction *= _force;
        _rigidbody.velocity = new(_rigidbody.velocity.x, direction);
    }
}
