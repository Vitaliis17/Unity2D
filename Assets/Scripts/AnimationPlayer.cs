using UnityEngine;

public class AnimationPlayer
{
    private readonly Animator _animator;
    private readonly int _defaultHash;

    private int _currentHash;

    public AnimationPlayer(Animator animator, int defaultHash)
    {
        _animator = animator;
        _defaultHash = defaultHash;

        PlayDefault();
    }

    public void PlayDefault()
        => PlayAnimation(_defaultHash);

    public void Play(int hash)
    {
        if (AnimationsPriority.IsMostPriority(hash, _currentHash) == false)
            return;

        PlayAnimation(hash);
    }

    private void PlayAnimation(int hash)
    {
        _currentHash = hash;
        _animator.Play(_currentHash);
    }
}