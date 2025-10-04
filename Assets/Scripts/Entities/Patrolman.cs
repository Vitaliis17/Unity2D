using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D), typeof(Rigidbody2D), typeof(Animator))]
public class Patrolman : MonoBehaviour
{
    [SerializeField, Min(0)] private float _speed;
    [SerializeField, Min(0)] private int _damage;

    [SerializeField] private Transform[] _targetPoints;
    [SerializeField] private Flipper _flipper;
    [SerializeField] private Health _health;
    [SerializeField] private AttackChecker _attackChecker;

    private Rigidbody2D _rigidbody;
    private Runner _runner;
    private Attacker _attacker;
    private Timer _timer;

    private AnimationPlayer _animationPlayer;

    private int _currentTargetIndex;
    private int _directionIndex;

    private int _movingDirection;

    private Coroutine _coroutine;

    private void Awake()
    {
        const int PositiveDirection = 1;

        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.freezeRotation = true;

        _runner = new(_speed);
        _attacker = new(_damage);
        _timer = new();

        Animator animator = GetComponent<Animator>();
        _animationPlayer = new(animator);

        _currentTargetIndex = 0;
        _directionIndex = PositiveDirection;
        _movingDirection = PositiveDirection;

        if (_targetPoints.Length == 0)
            _animationPlayer.SetDefault();
    }

    private void OnEnable()
    {
        _attackChecker.OnPlayerTriggered += Attack;
        _timer.TimePassed += _animationPlayer.SetDefault;
    }

    private void OnDisable()
    {
        _attackChecker.OnPlayerTriggered -= Attack;
        _timer.TimePassed -= _animationPlayer.SetDefault;
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

        _animationPlayer.Play(AnimationHashes.Running, ParameterHashes.IsRunning);
    }

    private void Attack(Player player)
    {
        if (_animationPlayer.GetParameter(ParameterHashes.IsAttacking))
            return;

        _animationPlayer.Play(AnimationHashes.Attacking, ParameterHashes.IsAttacking);

        _attacker.Attack(player.GetComponent<Health>());

        StartTimer();
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

    private void StartTimer()
    {
        float time = _animationPlayer.GetCurrentAnimationLength();

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(_timer.Wait(time));
    }
}