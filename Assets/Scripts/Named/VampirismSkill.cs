using System;
using System.Collections;

public class VampirismSkill
{
    private readonly Timer _timer;
    private readonly Vampirism _vampirism;
    
    private readonly int _healthAmount;

    private readonly int _reloadTime;
    private readonly int _activeTime;
    
    public Action Deactivation;

    public bool IsActiveVampirism {  get; private set; }
    public bool IsPossibleUsing { get; private set; }

    
    public VampirismSkill(Timer timer, int healthAmount, int reloadTime, int activeTime)
    {
        _timer = timer;

        _healthAmount = healthAmount;

        _reloadTime = reloadTime;
        _activeTime = activeTime;

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

    public IEnumerator StartUsing()
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