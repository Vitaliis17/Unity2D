using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D), typeof(Rigidbody2D), typeof(Animator))]
public class Patrolman : Human
{
    [SerializeField] private Transform[] _targetPoints;

    private int _currentTargetIndex;
    private int _directionIndex;

    private int _movingDirection;

    protected override void Awake()
    {
        const int PositiveDirection = 1;

        base.Awake();

        _currentTargetIndex = 0;
        _directionIndex = PositiveDirection;
        _movingDirection = PositiveDirection;
    }

    private void FixedUpdate()
    {
        int direction = ReadDirection();

        if (direction != _movingDirection)
        {
            RotateY();
            _movingDirection = direction;
        }

        Move(direction);

        if (IsReached(_targetPoints[_currentTargetIndex]))
            SetNextIndex();
    }

    protected override void Move(float direction)
    {
        const AnimationNames RunningAnimation = AnimationNames.Running;

        Runner.Move(Rigidbody, direction);

        if (direction == 0)
        {
            Player.PlayDefault();
            return;
        }

        Player.Play(RunningAnimation);
    }

    private int ReadDirection()
    {
        float difference = _targetPoints[_currentTargetIndex].transform.position.x - transform.position.x;
        return (int)Mathf.Sign(difference);
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