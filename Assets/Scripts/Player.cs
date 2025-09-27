using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField, Min(0)] private float _speed;
    [SerializeField, Min(0)] private float _jumpingForce;

    [SerializeField] private InputReader _input;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private Flipper _flipper;
    [SerializeField] private CoinTaker _coinTaker;

    private Runner _runner;
    private Jumper _jumper;

    private DirectionReversalHandler _directionReversalHandler;

    private AnimationPlayer _player;
    private Rigidbody2D _rigidbody;

    private bool _isJumping;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.freezeRotation = true;

        _runner = new(_speed);
        _jumper = new(_jumpingForce);

        Animator animator = GetComponent<Animator>();
        _player = new(animator, AnimationHashes.Idle);

        _directionReversalHandler = new();

        _isJumping = false;
    }

    private void OnEnable()
    {
        _input.Jumped += Jump;
        _input.Moved += Move;
        _input.Moved += _directionReversalHandler.UpdateDirectionSigns;

        _directionReversalHandler.DirectionChanged += _flipper.FlipY;
    }

    private void OnDisable()
    {
        _input.Jumped -= Jump;
        _input.Moved -= Move;
        _input.Moved -= _directionReversalHandler.UpdateDirectionSigns;

        _directionReversalHandler.DirectionChanged -= _flipper.FlipY;
    }

    private void Jump(float direction)
    {
        if (_groundChecker.IsGrounded == false)
            return;

        if (_isJumping && direction == 0)
        {
            _isJumping = false;
            _player.PlayDefault();
        }

        if (direction == 0)
            return;

        _jumper.Jump(_rigidbody, direction);
        _player.Play(AnimationHashes.Jumping);

        _isJumping = true;
    }

    private void Move(float direction)
    {
        _runner.Move(_rigidbody, direction);

        if (_groundChecker.IsGrounded == false || _isJumping)
            return;

        if (direction == 0)
        {
            _player.PlayDefault();
            return;
        }

        _player.Play(AnimationHashes.Running);
    }
}