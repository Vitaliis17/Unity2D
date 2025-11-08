using System.Collections.Generic;

public static class AnimationLayers
{
    private static readonly Dictionary<int, int> _values;

    static AnimationLayers()
    {
        _values = new()
        {
            { AnimationHashes.Landing, AnimatorLayers.Base },
            { AnimationHashes.Attacking, AnimatorLayers.Base },
            { AnimationHashes.StartingJumping, AnimatorLayers.Base },
            { AnimationHashes.Falling, AnimatorLayers.Base },
            { AnimationHashes.Running, AnimatorLayers.Base },
            { AnimationHashes.Idle, AnimatorLayers.Base },
            { AnimationHashes.Landing, AnimatorLayers.Base },

            { AnimationHashes.UsingVampirism, AnimatorLayers.Background},

            { AnimationHashes.Rotation, AnimatorLayers.Base}
        };
    }

    public static int ReadLayer(int animationHash)
        => _values[animationHash];
}