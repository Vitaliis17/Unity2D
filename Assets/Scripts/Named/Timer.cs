using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    public event Action WaitingEnded;
    public event Action<float> ValueChanged;

    public IEnumerator Wait(float time)
    {
        const int MinTime = 0;

        while (time > MinTime)
        {
            time -= Time.deltaTime;

            yield return null;

            ValueChanged?.Invoke(time);
        }

        WaitingEnded?.Invoke();
    }
}