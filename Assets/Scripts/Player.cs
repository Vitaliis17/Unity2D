using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private AxisInputHandler _input;

    [SerializeField] private TriggerHandler _triggerHandler;

    [SerializeField] private Runner _runner;
    [SerializeField] private Jumper _jumper;

    private DirectionReversalHandler _directionReversalHandler;
    private AnimationPlayer _animationPlayer;

    private bool _isGrounded;
    private bool _isJumping;

    private void Awake()
    {
        GetComponent<Rigidbody2D>().freezeRotation = true;

        _directionReversalHandler = new();

        Animator animator = GetComponent<Animator>();
        _animationPlayer = new(animator);

        _isJumping = false;
    }

    private void OnEnable()
    {
        _triggerHandler.Triggered += SetGrounded;

        _input.Jumped += Jump;
        _input.Moved += Move;
        _input.Moved += _directionReversalHandler.UpdateDirectionSigns;

        _directionReversalHandler.DirectionChanged += RotateY;
    }

    private void OnDisable()
    {
        _triggerHandler.Triggered -= SetGrounded;

        _input.Jumped -= Jump;
        _input.Moved -= Move;
        _input.Moved -= _directionReversalHandler.UpdateDirectionSigns;

        _directionReversalHandler.DirectionChanged -= RotateY;
    }

    private void Jump(float direction)
    {
        if (_isGrounded == false)
            return;

        if (_isJumping && direction == 0)
        {
            _isJumping = false;
            _animationPlayer.PlayDefault();
        }

        if (direction == 0)
            return;

        _jumper.Jump(direction);
        _animationPlayer.Play(AnimationNames.Jumping);

        _isJumping = true;
    }

    private void Move(float direction)
    {
        _runner.Move(direction);

        if (_isGrounded == false || _isJumping)
            return;

        if (direction == 0)
        {
            _animationPlayer.PlayDefault();
            return;
        }

        _animationPlayer.Play(AnimationNames.Running);
    }

    private void RotateY()
    {
        const int AngleAmount = 180;

        transform.Rotate(new(0, AngleAmount));
    }

    private void SetGrounded(bool isGrounded)
        => _isGrounded = isGrounded;
}