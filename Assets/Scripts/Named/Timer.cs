using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float _waitingTime;
    private WaitForSeconds _waitingForSeconds;

    public event Action<float> MaxValueChanged;
    public event Action<float> CurrentValueChanged;

    public event Action OnValueChanged;

    private void Awake()
    {
        _waitingTime = Time.fixedDeltaTime;
        _waitingForSeconds = new(_waitingTime);
    }

    public IEnumerator Wait(float time)
    {
        const int MinTime = 0;

        MaxValueChanged?.Invoke(time);

        while (time > MinTime)
        {
            time -= _waitingTime;

            yield return _waitingForSeconds;

            OnValueChanged?.Invoke();
            CurrentValueChanged?.Invoke(time);
        }
    }

    public IEnumerator WaitReverse(float time)
    {
        const int MinTime = 0;

        float currentValue = MinTime;

        MaxValueChanged?.Invoke(time);

        while(currentValue < time)
        {
            currentValue += _waitingTime;

            yield return _waitingForSeconds;

            CurrentValueChanged?.Invoke(currentValue);
        }
    }
}