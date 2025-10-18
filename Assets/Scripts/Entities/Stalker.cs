using UnityEngine;

public class Stalker : MonoBehaviour
{
    [SerializeField] private Runner _runner;

    private Flipper _flipper;

    private void Awake()
        => _flipper = new();

    public void Move(Player target, Rigidbody2D rigidbody)
    {
        int direction = ReadDirection(rigidbody.position, target.transform.position);

        RotateY(direction);

        _runner.Move(rigidbody, direction);
    }

    private int ReadDirection(Vector2 currentPosition, Vector2 targetPosition)
    {
        float difference = targetPosition.x - currentPosition.x;
        return (int)Mathf.Sign(difference);
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