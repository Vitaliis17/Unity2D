using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);

    [SerializeField, Min(0)] private float _speed;

    private Rigidbody2D _rigidbody;

    private float _lastDirection;
    private float _currentDirection;

    public event Action<int> DirectionChanged;

    private void Awake()
        => _rigidbody = GetComponent<Rigidbody2D>();

    private void Update()
        => _currentDirection = Input.GetAxis(Horizontal);

    private void FixedUpdate()
        => Move();

    private void Move()
    {
        _currentDirection *= _speed * Time.fixedDeltaTime;
        _rigidbody.velocity = new(_currentDirection, _rigidbody.velocity.y);

        int currentDirectionSign = Math.Sign(_currentDirection);
        int lastDirectionSign = Math.Sign(_lastDirection);

        if (currentDirectionSign != lastDirectionSign)
            DirectionChanged?.Invoke(currentDirectionSign);

        _lastDirection = _currentDirection;
    }
}