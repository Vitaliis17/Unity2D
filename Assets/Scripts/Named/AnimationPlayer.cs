using UnityEngine;

public class AnimationPlayer
{
    private readonly Animator _animator;

    private readonly int _defaultAnimationHash;
    private readonly int _defaultParameterHash;

    private readonly int _layerIndex;

    private int _currentAnimationHash;
    private int _currentParameterHash;

    public AnimationPlayer(Animator animator, int layerIndex = 0)
    {
        _animator = animator;
        _layerIndex = layerIndex;

        _defaultAnimationHash = AnimationHashes.Idle;
        _defaultParameterHash = ParameterHashes.IsIdle;

        PlayDefault();
    }

    public void Play(int animationHash, int parameterHash)
    {
        if (ParametersPriority.IsMostPriority(parameterHash, _currentParameterHash) == false)
            return;

        SetParameter(parameterHash);
        PlayAnimation(animationHash, parameterHash);
    }

    public void SetDefault()
    {
        _animator.SetBool(_currentParameterHash, false);

        _currentParameterHash = _defaultParameterHash;
        _currentAnimationHash = _defaultAnimationHash;
    }

    public void SetGrounded(bool grounded)
        => _animator.SetBool(ParameterHashes.IsGrounded, grounded);

    public bool GetParameter(int hash)
        => _animator.GetBool(hash);

    public void TurnOffRunning()
        => _animator.SetBool(ParameterHashes.IsRunning, false);

    public float GetCurrentAnimationLength()
    {
        _animator.Update(0f);

        return _animator.GetCurrentAnimatorStateInfo(_layerIndex).length;
    }

    private void PlayDefault()
        => PlayAnimation(_defaultAnimationHash, _defaultParameterHash);

    private void SetParameter(int hash)
    {
        _animator.SetBool(_currentParameterHash, false);
        _animator.SetBool(hash, true);
        _currentParameterHash = hash;
    }

    private void PlayAnimation(int animationHash, int parameterHash)
    {
        _currentAnimationHash = animationHash;
        _currentParameterHash = parameterHash;

        _animator.Play(_currentAnimationHash);
    }
}