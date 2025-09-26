using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Player : Human
{
    [SerializeField, Min(0)] private float _jumpingForce;

    [SerializeField] private AxisInputHandler _input;
    [SerializeField] private GroundChecker _triggerHandler;

    private Jumper _jumper;
    private DirectionReversalHandler _directionReversalHandler;

    private bool _isGrounded;
    private bool _isJumping;

    private int _coinAmount;

    protected override void Awake()
    {
        base.Awake();

        _directionReversalHandler = new();
        _jumper = new(_jumpingForce);

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
            Take(coin);
    }

    private void Take(Coin coin)
    {
        _coinAmount++;
        Destroy(coin.gameObject);
    }

    private void Jump(float direction)
    {
        const AnimationNames JumpingAnimation = AnimationNames.Jumping;

        if (_isGrounded == false)
            return;

        if (_isJumping && direction == 0)
        {
            _isJumping = false;
            Player.PlayDefault();
        }

        if (direction == 0)
            return;

        _jumper.Jump(Rigidbody, direction);
        Player.Play(JumpingAnimation);

        _isJumping = true;
    }

    protected override void Move(float direction)
    {
        const AnimationNames RunningAnimation = AnimationNames.Running;

        Runner.Move(Rigidbody, direction);

        if (_isGrounded == false || _isJumping)
            return;

        if (direction == 0)
        {
            Player.PlayDefault();
            return;
        }

        Player.Play(RunningAnimation);
    }

    private void SetGrounded(bool isGrounded)
        => _isGrounded = isGrounded;
}