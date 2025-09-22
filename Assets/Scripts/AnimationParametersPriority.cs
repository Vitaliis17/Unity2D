using System.Collections.Generic;

public static class AnimationParametersPriority
{
    private static Dictionary<AnimationNames, int> _animationsPriority;

    static AnimationParametersPriority()
    {
        _animationsPriority = new Dictionary<AnimationNames, int>
        {
            { AnimationNames.Falling, 1},
            { AnimationNames.Jumping, 2},
            { AnimationNames.Running, 3 },
            { AnimationNames.Idle, 4 }
        };
    }

    public static int GetPriority(AnimationNames name)
        => _animationsPriority[name];

    public static bool IsMostPriority(AnimationNames first, AnimationNames second)
        => GetPriority(first) < GetPriority(second);
}