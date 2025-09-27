using UnityEngine;
using System;

public class InputReader : MonoBehaviour
{
    private Vector2 _direction;

    public event Action<float> Jumped;
    public event Action<float> Moved;

    private void Update()
        => _direction.Set(Input.GetAxis(nameof(AxisNames.Horizontal)), Input.GetAxis(nameof(AxisNames.Jump)));

    private void FixedUpdate()
    {
        Moved?.Invoke(_direction.x);
        Jumped?.Invoke(_direction.y);
    }
}