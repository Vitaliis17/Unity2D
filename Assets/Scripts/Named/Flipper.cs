using UnityEngine;

public class Flipper 
{
    public void FlipY(Transform transform)
    {
        const int AngleAmount = 180;

        transform.Rotate(Vector2.up * AngleAmount);
    }

    public void RotateLeftY(Transform transform)
    {
        const float LeftRotationAngle = 180f;

        transform.rotation = new(transform.rotation.x, LeftRotationAngle, transform.rotation.z, transform.rotation.w);
    }

    public void RotateRightY(Transform transform)
    {
        const float RightRotationAngle = 0f;

        transform.rotation = new(transform.rotation.x, RightRotationAngle, transform.rotation.z, transform.rotation.w);
    }
}