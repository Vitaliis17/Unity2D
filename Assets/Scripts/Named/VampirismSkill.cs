using System;
using System.Collections;
using UnityEngine;

public class VampirismSkill : MonoBehaviour
{
    [SerializeField, Min(0)] private int _healthAmount;

    [SerializeField, Min(0)] private int _reloadTime;
    [SerializeField, Min(0)] private int _activeTime;

    [SerializeField] private ClickButtonsHandler _clickButtonsHandler;
    [SerializeField] private ZoneChecker _vampirismChecker;

    [SerializeField] private Health _health;
    [SerializeField] private Timer _timer;
    
    private Vampirism _vampirism;
    private Coroutine _coroutine;

    public event Action Activation;
    public event Action Deactivation;

    public bool IsActive {  get; private set; }
    
    private void Awake()
    {
        _vampirism = new();

        IsActive = false;
    }

    private void OnEnable()
    {
        _clickButtonsHandler.FirstSideMouseButtonPressed += StartUsing;
        _timer.OnValueChanged += Use;
    }

    private void OnDisable()
    {
        _clickButtonsHandler.FirstSideMouseButtonPressed -= StartUsing;
        _timer.OnValueChanged -= Use;
    }

    private void Use()
    {
        if (IsActive == false)
            return;

        Collider2D enemy = _vampirismChecker.ReadCollider();

        if (enemy != null && enemy.TryGetComponent(out Health enemyHealth))
            _vampirism.Transfer(_health, enemyHealth, _healthAmount);
    }

    private void StartUsing()
    {
        if (_coroutine != null)
            return;

        _coroutine = StartCoroutine(StartTimers());
    }

    private IEnumerator StartTimers()
    {
        yield return Activate();
        yield return Reload();

        _coroutine = null;
    }

    private IEnumerator Activate()
    {
        IsActive = true;

        Activation?.Invoke();

        yield return _timer.Wait(_activeTime);
    }

    private IEnumerator Reload()
    {
        IsActive = false;

        Deactivation?.Invoke();

        yield return _timer.WaitReverse(_reloadTime);
    }
}