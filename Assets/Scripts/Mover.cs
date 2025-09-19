using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);

    [SerializeField, Min(0)] private float _speed;

    private Rigidbody2D _rigidbody;
    private float _direction;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.freezeRotation = true;
    }

    private void Update()
        => _direction = Input.GetAxis(Horizontal);

    private void FixedUpdate()
    {
        _direction = _direction * _speed * Time.fixedDeltaTime;
        _rigidbody.velocity = new(_direction, _rigidbody.velocity.y);
    }
}
