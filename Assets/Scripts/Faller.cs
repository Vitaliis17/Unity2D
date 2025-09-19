using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Faller : MonoBehaviour
{
    [SerializeField, Min(0)] private float _fallMultiplier;

    private Rigidbody2D _rigidbody;

    private void Awake()
        => _rigidbody = GetComponent<Rigidbody2D>();

    private void FixedUpdate()
    {
        if (_rigidbody.velocity.y < 0)
            _rigidbody.velocity += Physics2D.gravity * _fallMultiplier * Time.fixedDeltaTime;
    }
}