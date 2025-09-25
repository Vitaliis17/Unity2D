using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(Animator))]
public class Coin : MonoBehaviour
{
    private CircleCollider2D _circleCollider;
    private AnimationPlayer _player;

    private void Awake()
    {
        const AnimationNames DefaultAnimation = AnimationNames.Rotation;

        _circleCollider = GetComponent<CircleCollider2D>();
        _circleCollider.isTrigger = true;

        Animator animator = GetComponent<Animator>();
        _player = new(animator, DefaultAnimation);
    }
}
