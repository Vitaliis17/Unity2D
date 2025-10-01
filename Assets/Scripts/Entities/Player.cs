using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField, Min(0)] private float _speed;
    [SerializeField, Min(0)] private float _jumpingForce;

    [SerializeField, Min(0)] private int _damage;

    [SerializeField] private InputReader _input;

    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private AttackChecker _attackChecker;

    [SerializeField] private Flipper _flipper;
    [SerializeField] private Collecter _collecter;

    [SerializeField] private Health _health;

    private Runner _runner;
    private Jumper _jumper;
    private Attacker _attacker;
    private AnimationPlayer _player;

    private DirectionReversalHandler _directionReversalHandler;

    private Rigidbody2D _rigidbody;

    private bool _isAttacking;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.freezeRotation = true;

        _runner = new(_speed);
        _jumper = new(_jumpingForce);
        _attacker = new(_damage);

        Animator animator = GetComponent<Animator>();
        _player = new(animator);

        _directionReversalHandler = new();
    }

    private void OnEnable()
    {
        _groundChecker.Triggered += SetGrounded;

        _input.Attacked += Attack;
        _input.Jumped += Jump;
        _input.Moved += Move;
        _input.Moved += _directionReversalHandler.UpdateDirectionSigns;

        _directionReversalHandler.DirectionChanged += _flipper.FlipY;
    }

    private void OnDisable()
    {
        _groundChecker.Triggered -= SetGrounded;

        _input.Attacked -= Attack;
        _input.Jumped -= Jump;
        _input.Moved -= Move;
        _input.Moved -= _directionReversalHandler.UpdateDirectionSigns;

        _directionReversalHandler.DirectionChanged -= _flipper.FlipY;
    }

    private void Jump(float direction)
    {
        if (_player.GetParameter(ParameterHashes.IsGrounded) == false || direction == 0)
            return;

        _jumper.Jump(_rigidbody, direction);
        _player.Play(AnimationHashes.StartingJumping, ParameterHashes.IsJumping);
    }

    private void Move(float direction)
    {
        _runner.Move(_rigidbody, direction);

        if (direction == 0)
            _player.TurnOffRunning();

        if (direction == 0 || _player.GetParameter(ParameterHashes.IsGrounded) == false || _jumper.IsJumping || _isAttacking)
            return;

        _player.Play(AnimationHashes.Running, ParameterHashes.IsRunning);
    }

    private void Attack(float direction)
    {
        if(direction == 0)
            return;

        _isAttacking = true;
        _player.Play(AnimationHashes.Attacking, ParameterHashes.IsAttacking);

        Collider2D[] colliders = _attackChecker.ReadEnemies();

        for (int i = 0; i < colliders.Length; i++)
            _attacker.Attack(colliders[i].GetComponent<Health>());
    }

    private void SetGrounded(bool isGrounded)
        => _player.SetGrounded(isGrounded);
}