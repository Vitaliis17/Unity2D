using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Slider))]
public class SmoothBar : MonoBehaviour
{
    [SerializeField] private Health _health;

    private Slider _slider;
    private Coroutine _coroutine;

    private void Awake()
        => _slider = GetComponent<Slider>();

    private void OnEnable()
    {
        _health.MaxValueChanged += SetMax;
        _health.CurrentValueChanged += SetValueSmoothly;
    }

    private void OnDisable()
    {
        _health.MaxValueChanged -= SetMax;
        _health.CurrentValueChanged -= SetValueSmoothly;
    }

    private void LateUpdate()
        => transform.rotation = Quaternion.identity;

    private void SetMax(int value)
        => _slider.maxValue = value;

    private void SetValueSmoothly(int value)
    {
        const int Delta = 1;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Fill(value, Delta));
    }

    private IEnumerator Fill(float value, float delta)
    {
        int target = (int)Mathf.Clamp(value, _slider.minValue, _slider.maxValue);

        while (_slider.value != target)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, target, delta);

            yield return null;
        }
    }
}