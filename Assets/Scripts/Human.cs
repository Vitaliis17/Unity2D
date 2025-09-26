using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public abstract class Human : MonoBehaviour
{
    [SerializeField, Min(0)] protected float Speed;

    protected Rigidbody2D Rigidbody;
    protected Runner Runner;

    protected AnimationPlayer Player;

    protected virtual void Awake()
    {
        const AnimationNames DefaultAnimation = AnimationNames.Idle;

        Rigidbody = GetComponent<Rigidbody2D>();
        Rigidbody.freezeRotation = true;

        Runner = new(Speed);

        Animator animator = GetComponent<Animator>();
        Player = new(animator, DefaultAnimation);
    }

    protected void RotateY()
    {
        const int AngleAmount = 180;

        transform.Rotate(Vector2.up * AngleAmount);
    }

    protected abstract void Move(float direction);
}