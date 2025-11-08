using System.Collections.Generic;

public static class ParametersPriority
{
    private static readonly Dictionary<int, int> _parametersPriority;

    static ParametersPriority()
    {
        _parametersPriority = new()
        {
            { ParameterHashes.IsAttacking, 1},
            { ParameterHashes.IsUsingVampirism, 1},
            { ParameterHashes.IsLanding, 2 },
            { ParameterHashes.IsStartingJumping, 3 },
            { ParameterHashes.IsFalling, 4 },
            { ParameterHashes.IsRunning, 5 },
            { ParameterHashes.IsIdle, 5 },

            { ParameterHashes.IsRotation, 1}
        };
    }

    public static int GetPriority(int hash)
        => _parametersPriority[hash];

    public static bool IsMostPriority(int first, int second)
        => GetPriority(first) <= GetPriority(second) && IsEquals(first, second) == false;

    private static bool IsEquals(int first, int second)
        => first == second;
}