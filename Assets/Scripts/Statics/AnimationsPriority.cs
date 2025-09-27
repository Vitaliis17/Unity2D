using System.Collections.Generic;

public static class AnimationsPriority
{
    private static readonly Dictionary<int, int> _animationsPriority;

    static AnimationsPriority()
    {
        _animationsPriority = new Dictionary<int, int>
        {
            { AnimationHashes.Rotation, 1},
            { AnimationHashes.Jumping, 1},
            { AnimationHashes.Running, 2 },
            { AnimationHashes.Idle, 3 },
        };
    }

    public static int GetPriority(int hash)
        => _animationsPriority[hash];

    public static bool IsMostPriority(int first, int second)
        => GetPriority(first) < GetPriority(second);
}