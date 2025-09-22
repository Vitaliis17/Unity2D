using UnityEngine;

public class AnimationPlayer
{
    private Animator _animator;

    private AnimationNames _currentAnimation;

    public AnimationPlayer(Animator animator)
    {
        _animator = animator;
        _currentAnimation = AnimationNames.Idle;

        SetParameter(true);
        PlayAnimation();
    }

    public void PlayDefault()
    {
        if (_currentAnimation != 0)
            SetParameter(false);

        _currentAnimation = AnimationNames.Idle;

        SetParameter(true);
        PlayAnimation();
    }

    public void Play(AnimationNames name = AnimationNames.Idle)
    {
        if (AnimationParametersPriority.IsMostPriority(name, _currentAnimation) == false)
            return;

        SetParameter(false);

        _currentAnimation = name;

        SetParameter(true);
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        int hash = AnimationHashes.GetHashName(_currentAnimation);
        _animator.Play(hash);
    }

    private void SetParameter(bool isEnabled)
    {
        int hash = AnimationHashes.GetHashParameter(_currentAnimation);
        _animator.SetBool(hash, isEnabled);
    }
}