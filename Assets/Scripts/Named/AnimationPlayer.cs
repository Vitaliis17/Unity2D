using UnityEngine;
using System.Collections.Generic;

public class AnimationPlayer
{
    private readonly Animator _animator;

    private readonly Dictionary<int, int?> _currentLayerParameters;

    public AnimationPlayer(Animator animator)
    {
        _animator = animator;
        _currentLayerParameters = new();

        for (int i = 0; i < _animator.layerCount; i++)
            _currentLayerParameters.Add(i, null);
    }

    public void Play(int animationHash, int parameterHash)
    {
        int layer = AnimationLayers.ReadLayer(animationHash);

        if (_currentLayerParameters[layer] != null && ParametersPriority.IsMostPriority(parameterHash, _currentLayerParameters[layer].Value) == false)
            return;

        SetParameter(parameterHash, layer);
        PlayAnimation(animationHash, parameterHash, layer);
    }

    public bool IsPlaying(int layer)
    {
        const float FinishedAnimationCoefficient = 1f;

        _animator.Update(0f);
        AnimatorStateInfo state = _animator.GetCurrentAnimatorStateInfo(layer);

        return (state.normalizedTime <= FinishedAnimationCoefficient && state.loop == false) || state.loop;
    }

    public void TurnOffAnimation(int animationHash, int parameterHash)
    {
        int layer = AnimationLayers.ReadLayer(animationHash);

        _animator.Update(0f);

        if (_currentLayerParameters[layer] == parameterHash)
            SetDefault(layer);
    }

    public void SetDefault(int layer)
    {
        if (_currentLayerParameters[layer] != null)
            _animator.SetBool(_currentLayerParameters[layer].Value, false);

        _currentLayerParameters[layer] = null;
    }

    public void ActivateGrounded()
        => _animator.SetBool(ParameterHashes.IsGrounded, true);

    public void DeactivateGrounded()
        => _animator.SetBool(ParameterHashes.IsGrounded, false);

    public bool GetParameter(int hash)
        => _animator.GetBool(hash);

    public void SetDefaultFreeLayers()
    {
        for (int i = 0; i < _animator.layerCount; i++)
        {
            if (IsPlaying(i) == false)
            {
                SetDefault(i);
            }
        }
    }

    private void SetParameter(int hash, int layer)
    {
        if (_currentLayerParameters[layer] != null)
            _animator.SetBool(_currentLayerParameters[layer].Value, false);

        _animator.SetBool(hash, true);
        _currentLayerParameters[layer] = hash;
    }

    private void PlayAnimation(int animationHash, int parameterHash, int layer)
    {
        _currentLayerParameters[layer] = parameterHash;

        _animator.Update(0f);
        _animator.Play(animationHash, layer);
    }
}