using UnityEngine;

public class AnimationPlayer
{
    private readonly Animator _animator;

    private readonly int _layerIndex;

    private int? _currentAnimationHash;
    private int? _currentParameterHash;

    public AnimationPlayer(Animator animator, int layerIndex = 0)
    {
        _animator = animator;
        _layerIndex = layerIndex;

        _currentAnimationHash = null;
        _currentParameterHash = null;
    }

    public void Play(int animationHash, int parameterHash)
    {
        if (_currentParameterHash != null && ParametersPriority.IsMostPriority(parameterHash, _currentParameterHash.Value) == false)
            return;

        SetParameter(parameterHash);
        PlayAnimation(animationHash, parameterHash);
    }

    public bool IsPlaying()
    {
        const float FinishedAnimationCoefficient = 1f;

        _animator.Update(0f);
        AnimatorStateInfo state = _animator.GetCurrentAnimatorStateInfo(_layerIndex);

        return (state.normalizedTime <= FinishedAnimationCoefficient && state.loop == false) || state.loop;
    }

    public void SetDefault()
    {
        _animator.SetBool(ParameterHashes.IsAttacking, false);

        _currentParameterHash = null;
        _currentAnimationHash = null;
    }

    public void ActivateGrounded()
        => _animator.SetBool(ParameterHashes.IsGrounded, true);

    public void DeactivateGrounded()
        => _animator.SetBool(ParameterHashes.IsGrounded, false);

    public bool GetParameter(int hash)
        => _animator.GetBool(hash);

    private void SetParameter(int hash)
    {
        if (_currentAnimationHash != null)
            _animator.SetBool(_currentParameterHash.Value, false);

        _animator.SetBool(hash, true);
        _currentParameterHash = hash;
    }

    private void PlayAnimation(int animationHash, int parameterHash)
    {
        _currentAnimationHash = animationHash;
        _currentParameterHash = parameterHash;

        _animator.Update(0f);
        _animator.Play(_currentAnimationHash.Value);
    }
}