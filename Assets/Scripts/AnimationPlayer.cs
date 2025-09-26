using UnityEngine;

public class AnimationPlayer
{
    private readonly Animator _animator;
    private readonly AnimationNames _defaultAnimation;

    private AnimationNames _currentAnimation;

    public AnimationPlayer(Animator animator, AnimationNames defaultAnimation)
    {
        _animator = animator;
        _defaultAnimation = defaultAnimation;

        PlayDefault();
    }

    public void PlayDefault()
        => PlayAnimation(_defaultAnimation);

    public void Play(AnimationNames name)
    {
        if (AnimationsPriority.IsMostPriority(name, _currentAnimation) == false)
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