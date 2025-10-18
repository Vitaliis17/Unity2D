using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D), typeof(Rigidbody2D), typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField, Min(0)] private int _damage;

    [SerializeField] private Health _health;
    [SerializeField] private ZoneChecker _attackChecker;
    [SerializeField] private ZoneChecker _viewChecker;

    [SerializeField] private Patrolman _patrolman;
    [SerializeField] private Stalker _stalker;

    private Rigidbody2D _rigidbody;
    private Attacker _attacker;

    private AnimationPlayer _animationPlayer;

    private Coroutine _coroutine;

    public event Action<Enemy> Releasing;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.freezeRotation = true;

        _attacker = new(_damage);

        Animator animator = GetComponent<Animator>();
        _animationPlayer = new(animator);
    }

    private void OnEnable()
        => _health.Died += Die;

    private void OnDisable()
        => _health.Died -= Die;

    private void FixedUpdate()
    {
        Collider2D playerCollider = _attackChecker.ReadCollider();

        if (playerCollider && playerCollider.TryGetComponent(out Player player))
            Attack(player);

        playerCollider = _viewChecker.ReadCollider();
        Move(playerCollider);
    }

    private void Move(Collider2D target)
    {
        if (target && target.TryGetComponent(out Player player))
            _stalker.Move(player, _rigidbody);
        else
            _patrolman.Move(_rigidbody);

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