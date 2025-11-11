using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField, Min(0)] private float _jumpingForce;

    [SerializeField, Min(0)] private int _damage;

    [SerializeField] private InputAxisReader _inputAxis;
    [SerializeField] private ClickButtonsHandler _clickButtonsHandler;

    [SerializeField] private ZoneChecker _groundChecker;
    [SerializeField] private ZoneChecker _attackChecker;
    [SerializeField] private ZoneChecker _itemChecker;

    [SerializeField] private Runner _runner;
    [SerializeField] private Health _health;
    [SerializeField] private VampirismSkill _vampirism;

    private Collecter _collecter;
    private ItemTaker _taker;

    private Flipper _flipper;

    private Jumper _jumper;
    private Attacker _attacker;
    private AnimationPlayer _animationPlayer;

    private DirectionReversalHandler _directionReversalHandler;

    private Rigidbody2D _rigidbody;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.freezeRotation = true;

        _collecter = new();
        _taker = new();

        _flipper = new();

        _jumper = new(_jumpingForce);
        _attacker = new(_damage);

        Animator animator = GetComponent<Animator>();
        _animationPlayer = new(animator);

        _directionReversalHandler = new();
    }

    private void OnEnable()
    {
        _clickButtonsHandler.LeftMouseButtonPressed += Attack;

        _inputAxis.Jumped += Jump;
        _inputAxis.Moved += Move;
        _inputAxis.Moved += _directionReversalHandler.UpdateDirectionSigns;

        _directionReversalHandler.DirectionChanged += Flip;

        _vampirism.Activation += ActivateVampirism;
        _vampirism.Deactivation += DeactivateVampirism;
        _health.Died += () => Destroy(gameObject);
    }

    private void OnDisable()
    {
        _clickButtonsHandler.LeftMouseButtonPressed -= Attack;
        _clickButtonsHandler.FirstSideMouseButtonPressed -= ActivateVampirism;

        _inputAxis.Jumped -= Jump;
        _inputAxis.Moved -= Move;
        _inputAxis.Moved -= _directionReversalHandler.UpdateDirectionSigns;

        _directionReversalHandler.DirectionChanged -= Flip;

        _vampirism.Activation -= ActivateVampirism;
        _vampirism.Deactivation -= DeactivateVampirism;
    }

    private void FixedUpdate()
    {
        SetGroundedState();
        TakeItems();

        _animationPlayer.SetDefaultFreeLayers();
    }

    private void ActivateVampirism()
        => _animationPlayer.Play(AnimationHashes.UsingVampirism, ParameterHashes.IsUsingVampirism);

    private void DeactivateVampirism()
        => _animationPlayer.TurnOffAnimation(AnimationHashes.UsingVampirism, ParameterHashes.IsUsingVampirism);

    private void Jump(float direction)
    {
        if (direction == 0 || _animationPlayer.GetParameter(ParameterHashes.IsGrounded) == false)
            return;

        _jumper.Jump(_rigidbody, direction);

        _animationPlayer.Play(AnimationHashes.StartingJumping, ParameterHashes.IsStartingJumping);
    }

    private void Move(float direction)
    {
        _runner.Move(_rigidbody, direction);

        if (direction == 0)
        {
            _animationPlayer.Play(AnimationHashes.Idle, ParameterHashes.IsIdle);

            return;
        }

        _animationPlayer.Play(AnimationHashes.Running, ParameterHashes.IsRunning);
    }

    private void Attack()
    {
        if (_animationPlayer.GetParameter(ParameterHashes.IsAttacking))
            return;

        _animationPlayer.Play(AnimationHashes.Attacking, ParameterHashes.IsAttacking);

        Collider2D[] colliders = _attackChecker.ReadColliders();

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent(out Health health))
            {
                _attacker.Attack(health);
            }
        }
    }

    private void SetGroundedState()
    {
        bool isGrounded = _groundChecker.ReadCollider();

        if (isGrounded == false)
        {
            DeactivateGrounded();
            return;
        }

        if (_animationPlayer.GetParameter(ParameterHashes.IsGrounded) == isGrounded)
            return;

        ActivateGrounded();
    }

    private void ActivateGrounded()
    {
        _animationPlayer.ActivateGrounded();
        _animationPlayer.Play(AnimationHashes.Landing, ParameterHashes.IsLanding);
    }

    private void DeactivateGrounded()
    {
        _animationPlayer.DeactivateGrounded();
        _animationPlayer.Play(AnimationHashes.Falling, ParameterHashes.IsFalling);
    }

    private void TakeItems()
    {
        Collider2D[] colliders = _itemChecker.ReadColliders();
        _taker.Take(_collecter, _health, colliders);
    }

    private void Flip()
        => _flipper.FlipY(transform);
}