using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField, Min(0)] private float _speed;
    [SerializeField, Min(0)] private float _jumpingForce;

    [SerializeField, Min(0)] private int _damage;

    [SerializeField] private InputAxisReader _inputAxis;
    [SerializeField] private ClickButtonsHandler _clickButtonsHandler;

    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private AttackChecker _attackChecker;

    [SerializeField] private Flipper _flipper;
    [SerializeField] private Collecter _collecter;

    [SerializeField] private Health _health;

    private Runner _runner;
    private Jumper _jumper;
    private Attacker _attacker;
    private AnimationPlayer _animationPlayer;

    private Timer _timer;
    private DirectionReversalHandler _directionReversalHandler;

    private Rigidbody2D _rigidbody;

    private Coroutine _coroutine;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.freezeRotation = true;

        _runner = new(_speed);
        _jumper = new(_jumpingForce);
        _attacker = new(_damage);

        Animator animator = GetComponent<Animator>();
        _animationPlayer = new(animator);

        _timer = new();
        _directionReversalHandler = new();
    }

    private void OnEnable()
    {
        _groundChecker.Triggered += SetGrounded;

        _clickButtonsHandler.LeftMouseButtonPressed += Attack;
        _inputAxis.Jumped += Jump;
        _inputAxis.Moved += Move;
        _inputAxis.Moved += _directionReversalHandler.UpdateDirectionSigns;

        _timer.TimePassed += _animationPlayer.SetDefault;
        _directionReversalHandler.DirectionChanged += _flipper.FlipY;
    }

    private void OnDisable()
    {
        _groundChecker.Triggered -= SetGrounded;

        _clickButtonsHandler.LeftMouseButtonPressed -= Attack;
        _inputAxis.Jumped -= Jump;
        _inputAxis.Moved -= Move;
        _inputAxis.Moved -= _directionReversalHandler.UpdateDirectionSigns;

        _timer.TimePassed -= _animationPlayer.SetDefault;
        _directionReversalHandler.DirectionChanged -= _flipper.FlipY;
    }

    private void Jump(float direction)
    {
        if (direction == 0 || _animationPlayer.GetParameter(ParameterHashes.IsGrounded) == false)
            return;

        _jumper.Jump(_rigidbody, direction);
        _animationPlayer.Play(AnimationHashes.StartingJumping, ParameterHashes.IsJumping);
    }

    private void Move(float direction)
    {
        _runner.Move(_rigidbody, direction);

        if (direction == 0 || _animationPlayer.GetParameter(ParameterHashes.IsGrounded) == false)
        {
            _animationPlayer.TurnOffRunning();
            return;
        }

        _animationPlayer.Play(AnimationHashes.Running, ParameterHashes.IsRunning);
    }

    private void Attack()
    {
        if (_animationPlayer.GetParameter(ParameterHashes.IsAttacking))
            return;

        _animationPlayer.Play(AnimationHashes.Attacking, ParameterHashes.IsAttacking);

        Collider2D[] colliders = _attackChecker.ReadEnemies();

        for (int i = 0; i < colliders.Length; i++)
            _attacker.Attack(colliders[i].GetComponent<Health>());

        StartTimer();
    }

    private void SetGrounded(bool isGrounded)
    {
        if (isGrounded)
        {
            _animationPlayer.Play(AnimationHashes.Landing, ParameterHashes.IsLanding);
            StartTimer();
        }
        else
        {
            _animationPlayer.Play(AnimationHashes.Falling, ParameterHashes.IsFalling);
        }

        _animationPlayer.SetGrounded(isGrounded);
    }

    private void StartTimer()
    {
        float time = _animationPlayer.GetCurrentAnimationLength();

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(_timer.Wait(time));
    }
}