using UnityEngine;

public class AnimationPlayer
{
    private readonly Animator _animator;

    private int _currentAnimationHash;
    private int _currentParameterHash;

    public AnimationPlayer(Animator animator)
    {
        _currentAnimationHash = AnimationHashes.Idle;
        _currentParameterHash = ParameterHashes.IsIdle;

        _animator = animator;
    }

    public void SetDefault()
        => _animator.SetBool(_currentParameterHash, false);

    public void Play(int animationHash, int parameterHash)
    {
        if (AnimationsPriority.IsMostPriority(animationHash, _currentAnimationHash) == false)
            return;

        SetParameter(parameterHash);
        PlayAnimation(animationHash);
    }

    public void SetGrounded(bool grounded)
        => _animator.SetBool(ParameterHashes.IsGrounded, grounded);

    public bool GetParameter(int hash)
        => _animator.GetBool(hash);

    public void TurnOffRunning()
        => _animator.SetBool(ParameterHashes.IsRunning, false);

    private void SetParameter(int hash)
    {
        _animator.SetBool(_currentParameterHash, false);
        _animator.SetBool(hash, true);
        _currentParameterHash = hash;
    }

    private void PlayAnimation(int hash)
    {
        _currentAnimationHash = hash;
        _animator.Play(_currentAnimationHash);
    }
}