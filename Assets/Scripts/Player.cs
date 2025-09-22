using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private AxisInputHandler _runningInput;
    [SerializeField] private AxisInputHandler _jumpingInput;

    [SerializeField] private TriggerHandler _triggerHandler;

    [SerializeField] private Runner _runner;
    [SerializeField] private Jumper _jumper;

    [SerializeField] private Animator _animator;

    private DirectionReversalHandler _directionReversalHandler;
    private AnimationPlayer _animationPlayer;

    private float _runningDirection;
    private float _jumpingDirection;

    private bool _isGrounded;
    private bool _isJumping;

    private void Awake()
    {
        GetComponent<Rigidbody2D>().freezeRotation = true;

        _directionReversalHandler = new(transform.rotation.eulerAngles.y == 0);
        _animationPlayer = new(_animator);

        _isJumping = false;
    }

    private void OnEnable()
    {
        _runningInput.Moved += SetRunningDirection;
        _jumpingInput.Moved += SetJumpingDirection;

        _triggerHandler.Triggered += SetGrounded;
        _directionReversalHandler.DirectionChanged += RotateY;
    }

    private void OnDisable()
    {
        _runningInput.Moved -= SetRunningDirection;
        _jumpingInput.Moved -= SetJumpingDirection;

        _triggerHandler.Triggered -= SetGrounded;
        _directionReversalHandler.DirectionChanged -= RotateY;
    }

    private void FixedUpdate()
    {
        Move();

        if (_isGrounded)
        {
            if (_isJumping)
            {
                _isJumping = false;
                _animationPlayer.PlayDefault();
            }

            Jump();
        }

        _directionReversalHandler.UpdateDirectionSigns(_runningDirection);
    }

    private void SetRunningDirection(float direction)
        => _runningDirection = direction;

    private void SetJumpingDirection(float direction)
        => _jumpingDirection = direction;

    private void SetGrounded(bool isGrounded)
        => _isGrounded = isGrounded;

    private void Jump()
    {
        _jumper.Jump(_jumpingDirection);

        if (_jumpingDirection == 0)
            return;

        AnimationNames name = AnimationNames.Jumping;
        _animationPlayer.Play(name);

        _isJumping = true;
    }

    private void Move()
    {
        if (_runningDirection == 0)
        {
            PlayIdle();
            return;
        }

        _runner.Move(_runningDirection);

        AnimationNames name = AnimationNames.Running;
        _animationPlayer.Play(name);
    }

    private void PlayIdle()
        => _animationPlayer.PlayDefault();

    private void RotateY()
    {
        const int AngleAmount = 180;

        transform.Rotate(new(0, AngleAmount));
    }
}