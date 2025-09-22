using UnityEngine;

public static class AnimationHashes
{
    public static readonly int Idle = Animator.StringToHash(nameof(Idle));
    public static readonly int Running = Animator.StringToHash(nameof(Running));
    public static readonly int Jumping = Animator.StringToHash(nameof(Jumping));

    public static readonly int IsIdle = Animator.StringToHash(nameof(IsIdle));
    public static readonly int IsRunning = Animator.StringToHash(nameof(IsRunning));
    public static readonly int IsJumping = Animator.StringToHash(nameof(IsJumping));

    public static int GetHashName(AnimationNames name)
    {
        int hash = 0;

        switch (name)
        {
            case AnimationNames.Idle:
                hash = Idle;
                break;

            case AnimationNames.Running:
                hash = Running;
                break;

            case AnimationNames.Jumping:
                hash = Jumping;
                break;
        }

        return hash;
    }

    public static int GetHashParameter(AnimationNames name)
    {
        int hash = 0;

        switch (name)
        {
            case AnimationNames.Idle:
                hash = IsIdle;
                break;

            case AnimationNames.Running:
                hash = IsRunning;
                break;

            case AnimationNames.Jumping:
                hash = IsJumping;
                break;
        }

        return hash;
    }
}