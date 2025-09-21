using UnityEngine;

public static class AnimationHashes
{
    public static readonly int IdleRight = Animator.StringToHash(nameof(IdleRight));
    public static readonly int IdleLeft = Animator.StringToHash(nameof(IdleLeft));

    public static readonly int RunningRight = Animator.StringToHash(nameof(RunningRight));
    public static readonly int RunningLeft = Animator.StringToHash(nameof(RunningLeft));
}