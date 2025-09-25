using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private AxisInputHandler _input;

    [SerializeField] private GroundChecker _triggerHandler;

    [SerializeField, Min(0)] private float _speed;
    [SerializeField, Min(0)] private float _jumpingForce;

    private Rigidbody2D _rigidbody;

    private Runner _runner;
    private Jumper _jumper;

    private DirectionReversalHandler _directionReversalHandler;
    private AnimationPlayer _animationPlayer;

    private bool _isGrounded;
    private bool _isJumping;

    private int _coinAmount;

    private void Awake()
    {
        const AnimationNames DefaultAnimation = AnimationNames.Idle;

        GetComponent<Rigidbody2D>().freezeRotation = true;

        _directionReversalHandler = new();

        _rigidbody = GetComponent<Rigidbody2D>();

        _runner = new(_speed);
        _jumper = new(_jumpingForce);

        Animator animator = GetComponent<Animator>();
        _animationPlayer = new(animator, DefaultAnimation);

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
            _animationPlayer.PlayDefault();
        }

        if (direction == 0)
            return;

        _jumper.Jump(_rigidbody, direction);
        _animationPlayer.Play(JumpingAnimation);

        _isJumping = true;
    }

    private void Move(float direction)
    {
        const AnimationNames RunningAnimation = AnimationNames.Running;

        _runner.Move(_rigidbody, direction);

        if (_isGrounded == false || _isJumping)
            return;

        if (direction == 0)
        {
            _animationPlayer.PlayDefault();
            return;
        }

        _animationPlayer.Play(RunningAnimation);
    }

    private void RotateY()
    {
        const int AngleAmount = 180;

        transform.Rotate(new(0, AngleAmount));
    }

    private void SetGrounded(bool isGrounded)
        => _isGrounded = isGrounded;
}