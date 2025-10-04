using System;
using UnityEngine;
using System.Collections;

public class Timer
{
    public event Action TimePassed;

    public IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);

        TimePassed?.Invoke();
    }
}