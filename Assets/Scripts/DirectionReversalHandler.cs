using System;

public class DirectionReversalHandler 
{
    private int _currentDirectionSign;

    private bool _isPositiveDirection;

    public event Action DirectionChanged;

    public DirectionReversalHandler(bool isPositiveDirection)
        => _isPositiveDirection = isPositiveDirection;

    public void UpdateDirectionSigns(float direction)
    {
        _currentDirectionSign = Math.Sign(direction);

        if (_currentDirectionSign == 0)
            return;

        if(IsSignSwapping())
        {
            DirectionChanged?.Invoke();

            _isPositiveDirection = _currentDirectionSign > 0;
        }
    }

    private bool IsSignSwapping()
        => _isPositiveDirection != _currentDirectionSign > 0;
}