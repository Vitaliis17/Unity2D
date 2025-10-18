using UnityEngine;

public class Patrolman : MonoBehaviour
{
    [SerializeField] private Runner _runner;

    private Flipper _flipper;
    private TargetPoint[] _targetPoints;

    private int _currentTargetIndex;
    private int _directionIndex;

    private void Awake()
    {
        const int PositiveDirection = 1;

        _flipper = new();
        _targetPoints = new TargetPoint[0];

        _currentTargetIndex = 0;
        _directionIndex = PositiveDirection;
    }

    public void Move(Rigidbody2D rigidbody)
    {
        if (_targetPoints.Length == 0)
            return;

        int direction = ReadDirection(rigidbody.position, _targetPoints[_currentTargetIndex].transform.position);
        RotateY(direction);

        _runner.Move(rigidbody, direction);

        if (IsReached(rigidbody))
            SetNextIndex();
    }

    public void AddTargetPoints(params TargetPoint[] targetPoints)
    {
        TargetPoint[] temp = new TargetPoint[_targetPoints.Length + targetPoints.Length];

        for (int i = 0; i < _targetPoints.Length; i++)
            temp[i] = _targetPoints[i];

        for (int i = _targetPoints.Length; i < temp.Length; i++)
            temp[i] = targetPoints[i - _targetPoints.Length];

        _targetPoints = temp;
    }

    private int ReadDirection(Vector2 currentPosition, Vector2 targetPosition)
    {
        float difference = targetPosition.x - currentPosition.x;
        return (int)Mathf.Sign(difference);
    }

    private void SetNextIndex()
    {
        _currentTargetIndex += _directionIndex;

        if (_currentTargetIndex == _targetPoints.Length - 1 || _currentTargetIndex == 0)
            ReverseDirectionIndex();
    }

    private bool IsReached(Rigidbody2D rigidbdoy)
    {
        const float Offset = 0.2f;

        return (rigidbdoy.position - (Vector2)_targetPoints[_currentTargetIndex].transform.position).sqrMagnitude < Offset;
    }

    private void ReverseDirectionIndex()
    {
        const int ReversingMultiply = -1;

        _directionIndex *= ReversingMultiply;
    }

    private void RotateY(int direction)
    {
        if (direction == 1)
        {
            _flipper.RotateRightY(transform);
            return;
        }

        _flipper.RotateLeftY(transform);
    }
}