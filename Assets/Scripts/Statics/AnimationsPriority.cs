using System.Collections.Generic;

public static class AnimationsPriority
{
    private static readonly Dictionary<AnimationNames, int> _animationsPriority;

    static AnimationsPriority()
    {
        _animationsPriority = new Dictionary<AnimationNames, int>
        {
            { AnimationNames.Rotation, 1},
            { AnimationNames.Jumping, 1},
            { AnimationNames.Running, 2 },
            { AnimationNames.Idle, 3 },
        };
    }

    public static int GetPriority(AnimationNames name)
        => _animationsPriority[name];

    public static bool IsMostPriority(AnimationNames first, AnimationNames second)
        => GetPriority(first) < GetPriority(second);
}