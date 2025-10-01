using UnityEngine;

public class Flipper : MonoBehaviour 
{
    public void FlipY()
    {
        const int AngleAmount = 180;

        transform.Rotate(Vector2.up * AngleAmount);
    }
}