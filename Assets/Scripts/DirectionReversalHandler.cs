using System;

public class DirectionReversalHandler 
{
    private bool _isPositiveDirection;

    public event Action DirectionChanged;

    public DirectionReversalHandler(bool isPositiveDirection = true)
        => _isPositiveDirection = isPositiveDirection;

    public void UpdateDirectionSigns(float direction)
    {
        if(direction == 0)
            return;

        if(IsSignSwapping(direction))
        {
            DirectionChanged?.Invoke();

            _isPositiveDirection = _isPositiveDirection == false;
        }
    }

    private bool IsSignSwapping(float direction)
        => _isPositiveDirection != Math.Sign(direction) > 0;
}