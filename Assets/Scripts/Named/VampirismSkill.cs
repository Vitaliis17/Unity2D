using UnityEngine;
using System;

public class VampirismSkill : MonoBehaviour
{
    [SerializeField] private int _healthAmount;

    [SerializeField] private float _reloadTime;
    [SerializeField] private int _activeTime;

    private Vampirism _vampirism;
    
    private Timer _reloader;
    private Timer _activityTimer;
    
    private Coroutine _coroutine;

    public event Action<int> MaxValueChanged;
    public event Action<float> CurrentValueChanged;

    public Action Deactivation;

    public bool IsActive {  get; private set; }
    public bool IsPossibleUsing { get; private set; }

    private void Awake()
    {
        _vampirism = new();

        _reloader = new();
        _activityTimer = new();

        IsActive = false;
        ActivatePossibleUsing();
    }

    private void OnEnable()
    {
        _activityTimer.WaitingEnded += Deactivate;
        _activityTimer.ValueChanged += InvokeCurrentTimeChanged;

        _reloader.WaitingEnded += ActivatePossibleUsing;
    }

    private void OnDisable()
    {
        _activityTimer.WaitingEnded -= Deactivate;
        _activityTimer.ValueChanged -= InvokeCurrentTimeChanged;

        _reloader.WaitingEnded -= ActivatePossibleUsing;
    }

    public void Use(Health taker, Health giver)
        => _vampirism.Transfer(taker, giver, _healthAmount);

    public void Activate()
    {
        IsActive = true;
        IsPossibleUsing = false;

        if (_coroutine != null)
            _coroutine = null;

        MaxValueChanged?.Invoke(_activeTime);
        _coroutine = StartCoroutine(_activityTimer.Wait(_activeTime));
    }

    private void InvokeCurrentTimeChanged(float time)
        => CurrentValueChanged?.Invoke(time);

    private void Deactivate()
    {
        IsActive = false;

        Deactivation?.Invoke();
        Reload();
    }

    private void Reload()
    {
        if(_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(_reloader.Wait(_reloadTime));
    }

    private void ActivatePossibleUsing()
        => IsPossibleUsing = true;
}