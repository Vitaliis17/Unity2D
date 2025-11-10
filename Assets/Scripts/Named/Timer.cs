using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public event Action<float> MaxValueChanged;
    public event Action<float> CurrentValueChanged;

    public IEnumerator Wait(float time)
    {
        const int MinTime = 0;

        MaxValueChanged?.Invoke(time);

        while (time > MinTime)
        {
            time -= Time.deltaTime;

            yield return null;

            CurrentValueChanged?.Invoke(time);
        }
    }

    public IEnumerator WaitReverse(float time)
    {
        const int MinTime = 0;

        float currentValue = MinTime;

        MaxValueChanged?.Invoke(time);
        CurrentValueChanged?.Invoke(currentValue);

        while(currentValue < time)
        {
            currentValue += Time.deltaTime;

            yield return null;

            CurrentValueChanged?.Invoke(currentValue);
        }
    }
}