using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);

    [SerializeField] private TriggerHandler _triggerHandler;

    [SerializeField, Min(0)] private float _speed;
    [SerializeField, Min(0)] private float _runningSpeed;

    private Rigidbody2D _rigidbody;

    private float _direction;

    private bool _isRunning;
    private bool _isGrounded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.freezeRotation = true;
    }

    private void OnEnable()
        => _triggerHandler.Triggered += SetGrounded;

    private void OnDisable()
        => _triggerHandler.Triggered -= SetGrounded;

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
            _isRunning = true;

        _direction = Input.GetAxis(Horizontal);
    }

    private void FixedUpdate()
    {
        _direction *= _speed * Time.fixedDeltaTime;

        if(_isRunning && _isGrounded)
        {
            _direction *= _runningSpeed;
            _isRunning = false;
        }

        _rigidbody.velocity = new(_direction, _rigidbody.velocity.y);
    }

    private void SetGrounded(bool isGrounded)
        => _isGrounded = isGrounded;
}
