using System.Collections.Generic;

public static class ParametersPriority
{
    private static readonly Dictionary<int, int> _parametersPriority;

    static ParametersPriority()
    {
        _parametersPriority = new Dictionary<int, int>
        {
            { ParameterHashes.IsAttacking, 1},
            { ParameterHashes.IsLanding, 2 },
            { ParameterHashes.IsJumping, 3 },
            { ParameterHashes.IsFalling, 4 },
            { ParameterHashes.IsRunning, 5 },
            { ParameterHashes.IsIdle, 6 }
        };
    }

    public static int GetPriority(int hash)
        => _parametersPriority[hash];

    public static bool IsMostPriority(int first, int second)
        => GetPriority(first) < GetPriority(second);
}