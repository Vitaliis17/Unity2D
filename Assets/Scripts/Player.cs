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
        _runningInput.Moved += Move;
        _runningInput.Moved += _directionReversalHandler.UpdateDirectionSigns;

        _directionReversalHandler.DirectionChanged += RotateY;

        _jumpingInput.Moved += Jump;

        _triggerHandler.Triggered += SetGrounded;
    }

    private void OnDisable()
    {
        _runningInput.Moved -= Move;
        _runningInput.Moved -= _directionReversalHandler.UpdateDirectionSigns;

        _directionReversalHandler.DirectionChanged -= RotateY;

        _jumpingInput.Moved -= Jump;

        _triggerHandler.Triggered -= SetGrounded;
    }

    private void SetGrounded(bool isGrounded)
        => _isGrounded = isGrounded;

    private void Jump(float direction)
    {
        if (_isGrounded == false)
            return;

        if (_isJumping && direction == 0)
        {
            _isJumping = false;
            _animationPlayer.PlayDefault();
        }

        _jumper.Jump(direction);

        if (direction == 0)
            return;

        AnimationNames name = AnimationNames.Jumping;
        _animationPlayer.Play(name);

        _isJumping = true;
    }

    private void Move(float direction)
    {
        if (direction == 0 && _isGrounded && _isJumping == false)
        {
            _animationPlayer.PlayDefault();
            return;
        }

        _runner.Move(direction);

        if (_isGrounded == false)
            return;

        AnimationNames name = AnimationNames.Running;
        _animationPlayer.Play(name);
    }

    private void RotateY()
    {
        const int AngleAmount = 180;

        transform.Rotate(new(0, AngleAmount));
    }
}