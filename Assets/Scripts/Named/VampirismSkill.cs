using System;
using System.Collections;
using UnityEngine;

public class VampirismSkill : MonoBehaviour
{
    [SerializeField, Min(0)] private int _healthAmount;

    [SerializeField, Min(0)] private int _reloadTime;
    [SerializeField, Min(0)] private int _activeTime;
    
    [SerializeField] private Timer _timer;
    
    private Vampirism _vampirism;
    private Coroutine _coroutine;

    public event Action Deactivation;

    public bool IsActiveVampirism {  get; private set; }
    public bool IsPossibleUsing { get; private set; }
    
    private void Awake()
    {
        _vampirism = new();

        IsActiveVampirism = false;
        IsPossibleUsing = true;
    }

    public bool TryUse(Health taker, Health giver)
    {
        if (IsActiveVampirism == false)
            return false;

        _vampirism.Transfer(taker, giver, _healthAmount);

        return true;
    }

    public void StartUsing()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(StartTimers());
    }

    private IEnumerator StartTimers()
    {
        yield return Activate();
        yield return Reload();
    }

    private IEnumerator Activate()
    {
        IsActiveVampirism = true;
        IsPossibleUsing = false;

        yield return _timer.Wait(_activeTime);
    }

    private IEnumerator Reload()
    {
        IsActiveVampirism = false;

        Deactivation?.Invoke();

        yield return _timer.WaitReverse(_reloadTime);

        IsPossibleUsing = true;
    }
}