using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(CapsuleCollider2D), typeof(Rigidbody2D), typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField, Min(0)] private float _speed;
    [SerializeField, Min(0)] private int _damage;

    [SerializeField] private Flipper _flipper;
    [SerializeField] private Health _health;
    [SerializeField] private ZoneChecker _attackChecker;
    [SerializeField] private ZoneChecker _viewChecker;

    private Transform[] _targetPoints;

    private Rigidbody2D _rigidbody;
    private Runner _runner;
    private Attacker _attacker;

    private AnimationPlayer _animationPlayer;

    private Player _enemy;

    private int _currentTargetIndex;
    private int _directionIndex;

    private int _movingDirection;

    private Coroutine _coroutine;

    public event Action<Enemy> Releasing;

    private void Awake()
    {
        const int PositiveDirection = 1;

        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.freezeRotation = true;

        _targetPoints = new Transform[0];

        _runner = new(_speed);
        _attacker = new(_damage);

        Animator animator = GetComponent<Animator>();
        _animationPlayer = new(animator);

        _currentTargetIndex = 0;
        _directionIndex = PositiveDirection;
        _movingDirection = PositiveDirection;
    }

    private void OnEnable()
        => _health.Died += Die;

    private void FixedUpdate()
    {
        Collider2D playerCollider = _attackChecker.ReadCollider();

        if (playerCollider && playerCollider.TryGetComponent(out Player player))
            Attack(player);

        Transform target = _viewChecker.ReadCollider()?.transform;

        if (target == null && _targetPoints[_currentTargetIndex] == null)
        {
            SetNextIndex();
            return;
        }

        if (target == null)
            target = _targetPoints[_currentTargetIndex];

        int direction = ReadDirection(target);

        if (direction != _movingDirection)
        {
            _flipper.FlipY();
            _movingDirection = direction;
        }

        Move(direction);

        if (_enemy == null && IsReached(_targetPoints[_currentTargetIndex]))
            SetNextIndex();
    }

    private void OnDisable()
        => _health.Died -= Die;

    public void AddTargetPoints(params TargetPoint[] targetPoints)
    {
        Transform[] temp = new Transform[_targetPoints.Length + targetPoints.Length];

        for (int i = 0; i < _targetPoints.Length; i++)
            temp[i] = _targetPoints[i];

        for (int i = _targetPoints.Length; i < temp.Length; i++)
            temp[i] = targetPoints[i - _targetPoints.Length].transform;

        _targetPoints = temp;
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

        if (player.TryGetComponent(out Health health))
            _attacker.Attack(health);

        StartNewCoroutine(WaitEndingAnimationState());
    }

    private int ReadDirection(Transform target)
    {
        float difference = target.transform.position.x - transform.position.x;
        return (int)Mathf.Sign(difference);
    }

    private bool IsReached(Transform target)
    {
        const float Offset = 0.2f;

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

    private void Die()
        => Releasing?.Invoke(this);

    private IEnumerator WaitEndingAnimationState()
    {
        float time = _animationPlayer.GetCurrentAnimationLength();

        yield return new WaitForSeconds(time);

        _animationPlayer.SetDefault();
    }

    private void StartNewCoroutine(IEnumerator enumerator)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(enumerator);
    }
}