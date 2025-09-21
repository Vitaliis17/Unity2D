using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Jumper))]
public class Player : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private Jumper _jumper;

    private Animator _animator;

    private void Awake()
    {
        GetComponent<Rigidbody2D>().freezeRotation = true;

        _animator = GetComponent<Animator>();
        _animator.Play(AnimationHashes.IdleRight);
    }

    private void OnEnable()
        => _mover.DirectionChanged += Play;

    private void OnDisable()
        => _mover.DirectionChanged -= Play;

    private void Play(int direction)
    {
        int hash = direction > 0 ? AnimationHashes.RunningRight : AnimationHashes.RunningLeft;

        if (direction == 0)
            hash = AnimationHashes.IdleRight;

        _animator.Play(hash);
    }
}