using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D), typeof(Rigidbody2D), typeof(Animator))]
public class Patrolman : MonoBehaviour
{
    [SerializeField, Min(0)] private float _speed;

    [SerializeField] private Transform[] _targetPoints;
    [SerializeField] private Flipper _flipper;
    [SerializeField] private Health _health;

    private Rigidbody2D _rigidbody;
    private Runner _runner;

    private AnimationPlayer _player;

    private int _currentTargetIndex;
    private int _directionIndex;

    private int _movingDirection;

    private void Awake()
    {
        const int PositiveDirection = 1;

        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.freezeRotation = true;

        _runner = new(_speed);

        Animator animator = GetComponent<Animator>();
        _player = new(animator);

        _currentTargetIndex = 0;
        _directionIndex = PositiveDirection;
        _movingDirection = PositiveDirection;
    }

    private void FixedUpdate()
    {
        int direction = ReadDirection();

        if (direction != _movingDirection)
        {
            _flipper.FlipY();
            _movingDirection = direction;
        }

        Move(direction);

        if (IsReached(_targetPoints[_currentTargetIndex]))
            SetNextIndex();
    }

    private void Move(float direction)
    {
        _runner.Move(_rigidbody, direction);

        if (direction == 0)
        {
            //_player.PlayDefault();
            return;
        }

        _player.Play(AnimationHashes.Running, ParameterHashes.IsRunning);
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