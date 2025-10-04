using UnityEngine;

public static class ParameterHashes
{
    public static readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));
    public static readonly int IsRunning = Animator.StringToHash(nameof(IsRunning));
    public static readonly int IsIdle = Animator.StringToHash(nameof(IsIdle));
    public static readonly int IsJumping = Animator.StringToHash(nameof(IsJumping));
    public static readonly int IsLanding = Animator.StringToHash(nameof(IsLanding));
    public static readonly int IsFalling = Animator.StringToHash(nameof(IsFalling));
    public static readonly int IsAttacking = Animator.StringToHash(nameof(IsAttacking));
}