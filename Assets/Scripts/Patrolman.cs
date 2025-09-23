using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D), typeof(Rigidbody2D))]
public class Patrolman : MonoBehaviour 
{
    [SerializeField, Min(0)] private float _speed;

    [SerializeField] private Transform[] _targetPoints;

    private Rigidbody2D _rigidbody;
    private Runner _runner;

    private int _currentTargetIndex;
    private int _directionIndex;

    private void Awake()
    {
        const int PositiveDirection = 1;

        _runner = new(_speed);

        _currentTargetIndex = 0;
        _directionIndex = PositiveDirection;

        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        _runner.MoveTowards(_rigidbody, _targetPoints[_currentTargetIndex]);

        if (IsReached(_targetPoints[_currentTargetIndex]))
            SetNextIndex();
    }

    private bool IsReached(Transform target)
    {
        const float Offset = 0.1f;

        return (transform.position - target.position).sqrMagnitude < Offset;
    }

    private void SetNextIndex()
    {
        _currentTargetIndex += _directionIndex;

        if (_currentTargetIndex == _targetPoints.Length - 1 || _currentTargetIndex == 0)
            ReverseDirectionIndex();
    }

    private void ReverseDirectionIndex()
    {
        const int ReversingMultiply = -1;

        _directionIndex *= ReversingMultiply;
    }
}