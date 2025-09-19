using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private TriggerHandler _triggerHandler;

    [SerializeField, Min(0)] private float _force;

    private Rigidbody2D _rigidbody;

    private bool _isGrounded;
    private bool _isJumping;

    private void Awake()
        => _rigidbody = GetComponent<Rigidbody2D>();

    private void OnEnable()
        => _triggerHandler.Triggered += SetGrounded;

    private void OnDisable()
        => _triggerHandler.Triggered -= SetGrounded;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            _isJumping = true;
    }

    private void FixedUpdate()
        => Jump();

    private void Jump()
    {
        if (CanJump() == false)
            return;

        _rigidbody.velocity = new(_rigidbody.velocity.x, _force);
        _isJumping = false;
    }

    private bool CanJump()
        => _isGrounded && _isJumping;

    private void SetGrounded(bool isGrounded)
        => _isGrounded = isGrounded;
}
