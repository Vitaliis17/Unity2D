using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Slider))]
public class TimeSmoothBar : MonoBehaviour
{
    [SerializeField] private Timer _timer;

    private Slider _slider;
    private Coroutine _coroutine;

    private void Awake()
        => _slider = GetComponent<Slider>();

    private void OnEnable()
    {
        _timer.MaxValueChanged += SetMax;
        _timer.CurrentValueChanged += SetValueSmoothly;
    }

    private void OnDisable()
    {
        _timer.MaxValueChanged -= SetMax;
        _timer.CurrentValueChanged -= SetValueSmoothly;
    }

    private void LateUpdate()
        => transform.rotation = Quaternion.identity;

    private void SetMax(float value)
    {
        float t = Mathf.InverseLerp(_slider.minValue, _slider.maxValue, _slider.value);

        _slider.maxValue = value;

        float currentValue = Mathf.Lerp(_slider.minValue, _slider.maxValue, t);
        SetCurrentValue(currentValue);
    }

    private void SetCurrentValue(float value)
        => _slider.value = value;

    private void SetValueSmoothly(float value)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Fill(value, Time.deltaTime));
    }

    private IEnumerator Fill(float value, float delta)
    {
        float target = Mathf.Clamp(value, _slider.minValue, _slider.maxValue);

        while (_slider.value != target)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, target, delta);

            yield return null;
        }
    }
}