using UnityEngine;

public static class AnimationHashes
{
    public static readonly int Idle = Animator.StringToHash(nameof(Idle));
    public static readonly int Running = Animator.StringToHash(nameof(Running));
    public static readonly int Jumping = Animator.StringToHash(nameof(Jumping));

    public static readonly int Rotation = Animator.StringToHash(nameof(Rotation));
}