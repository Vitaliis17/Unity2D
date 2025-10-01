using System.Collections.Generic;

public static class AnimationsPriority
{
    private static readonly Dictionary<int, int> _animationsPriority;

    static AnimationsPriority()
    {
        _animationsPriority = new Dictionary<int, int>
        {
            { AnimationHashes.Rotation, 1},
            { AnimationHashes.Attacking, 1},
            { AnimationHashes.StartingJumping, 2},
            { AnimationHashes.EndingJumping, 2},
            { AnimationHashes.Running, 3 },
            { AnimationHashes.Idle, 4 }
        };
    }

    public static int GetPriority(int hash)
        => _animationsPriority[hash];

    public static bool IsMostPriority(int first, int second)
        => GetPriority(first) < GetPriority(second);
}