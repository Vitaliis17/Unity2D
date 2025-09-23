using UnityEngine;

public class AnimationPlayer
{
    private Animator _animator;

    private AnimationNames _currentAnimation;

    public AnimationPlayer(Animator animator)
    {
        _animator = animator;
        PlayDefault();
    }

    public void PlayDefault()
        => PlayAnimation(AnimationNames.Idle);

    public void Play(AnimationNames name)
    {
        if (AnimationParametersPriority.IsMostPriority(name, _currentAnimation) == false)
            return;

        PlayAnimation(name);
    }

    private void PlayAnimation(AnimationNames name)
    {
        _currentAnimation = name;

        int hash = AnimationHashes.GetHashName(_currentAnimation);
        _animator.Play(hash);
    }
}