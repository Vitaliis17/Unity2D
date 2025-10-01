using UnityEngine;

public static class AnimationHashes
{
    public static readonly int Idle = Animator.StringToHash(nameof(Idle));
    public static readonly int Running = Animator.StringToHash(nameof(Running));
    public static readonly int StartingJumping = Animator.StringToHash(nameof(StartingJumping));
    public static readonly int EndingJumping = Animator.StringToHash(nameof(EndingJumping));
    public static readonly int Attacking = Animator.StringToHash(nameof(Attacking));

    public static readonly int Rotation = Animator.StringToHash(nameof(Rotation));
}