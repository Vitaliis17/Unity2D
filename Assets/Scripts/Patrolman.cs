using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D), typeof(Rigidbody2D), typeof(Animator))]
public class Patrolman : MonoBehaviour 
{
    [SerializeField, Min(0)] private float _speed;

    [SerializeField] private Transform[] _targetPoints;

    private Rigidbody2D _rigidbody;
    private Runner _runner;

    private AnimationPlayer _animationPlayer;

    private int _currentTargetIndex;
    private int _directionIndex;

    private int _movingDirection;

    private void Awake()
    {
        const AnimationNames DefaultAnimation = AnimationNames.Idle;
        const int PositiveDirection = 1;

        _runner = new(_speed);

        _currentTargetIndex = 0;
        _directionIndex = PositiveDirection;
        _movingDirection = PositiveDirection;

        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.freezeRotation = true;

        Animator animator = GetComponent<Animator>();
        _animationPlayer = new(animator, DefaultAnimation);
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

    private void Move(float direction)
    {
        const AnimationNames RunningAnimation = AnimationNames.Running;

        _runner.Move(_rigidbody, direction);

        if (direction == 0)
        {
            _animationPlayer.PlayDefault();
            return;
        }

        _animationPlayer.Play(RunningAnimation);
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

    private void RotateY()
    {
        const int AngleAmount = 180;

        transform.Rotate(new(0, AngleAmount));
    }

    private void ReverseDirectionIndex()
    {
        const int ReversingMultiply = -1;

        _directionIndex *= ReversingMultiply;
    }
}