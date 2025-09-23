using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D), typeof(Rigidbody2D))]
public class Patrolman : MonoBehaviour 
{
    [SerializeField] private Runner _runner;
    [SerializeField] private Transform[] _targetPoints;

    private int _currentTargetIndex;
    private int _directionIndex;

    private void Awake()
    {
        const int PositiveDirection = 1;

        _currentTargetIndex = 0;
        _directionIndex = PositiveDirection;

        GetComponent<Rigidbody2D>().freezeRotation = true;
    }

    private void FixedUpdate()
    {
        _runner.MoveTowards(_targetPoints[_currentTargetIndex]);

        if (IsReached(_targetPoints[_currentTargetIndex]))
            SetNextIndex();
    }

    private bool IsReached(Transform target)
        => Mathf.Approximately(transform.position.x, target.position.x);

    private void SetNextIndex()
    {
        _currentTargetIndex += _directionIndex;

        if (_currentTargetIndex == _targetPoints.Length - 1 || _currentTargetIndex == 0)
            ReverseDirectionIndex();
    }

    private void ReverseDirectionIndex()
    {
        const int ReversingMultiply = -1;

        _directionIndex *= ReversingMultiply;
    }
}