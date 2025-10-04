using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private void FixedUpdate()
    {
        if (_target != null)
            transform.position = new(_target.position.x, _target.position.y, transform.position.z);
    }
}