using System;
using System.Collections;
using UnityEngine;

public class Reloader : MonoBehaviour
{
    [SerializeField] private float _maxTime;

    private Coroutine _coroutine;

    public event Action<float> OnValueChanged;

    public void Start()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Wait());
    }

    public bool IsReady()
        => _coroutine == null;

    private IEnumerator Wait()
    {
        const int MinTime = 0;

        float time = _maxTime;

        while (time > MinTime)
        {
            time -= Time.deltaTime;

            yield return null;
        }

        _coroutine = null;
    }
}