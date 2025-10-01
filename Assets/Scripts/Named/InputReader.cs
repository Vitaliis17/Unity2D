using UnityEngine;
using System;

public class InputReader : MonoBehaviour
{
    private Vector2 _movementDirection;
    private float _fireDirection;

    public event Action<float> Jumped;
    public event Action<float> Moved;
    public event Action<float> Attacked;

    private void Update()
    {
        _movementDirection.Set(Input.GetAxis(nameof(AxisNames.Horizontal)), Input.GetAxis(nameof(AxisNames.Jump)));
        _fireDirection = Input.GetAxis(nameof(AxisNames.Fire1));
    }

    private void FixedUpdate()
    {
        Moved?.Invoke(_movementDirection.x);
        Jumped?.Invoke(_movementDirection.y);
        Attacked?.Invoke(_fireDirection);
    }
}