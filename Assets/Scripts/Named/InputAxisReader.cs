using UnityEngine;
using System;

public class InputAxisReader : MonoBehaviour
{
    private Vector2 _movementDirection;

    public event Action<float> Jumped;
    public event Action<float> Moved;

    private void Update()
        => _movementDirection.Set(Input.GetAxis(nameof(AxisNames.Horizontal)), Input.GetAxis(nameof(AxisNames.Jump)));

    private void FixedUpdate()
    {
        Moved?.Invoke(_movementDirection.x);
        Jumped?.Invoke(_movementDirection.y);
    }
}