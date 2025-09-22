using UnityEngine;
using System;

public class AxisInputHandler : MonoBehaviour
{
    [SerializeField] private AxisNames _axis;

    private float _direction;

    public event Action<float> Moved;

    private string _axisName;

    private void Awake()
        => _axisName = _axis.ToString();

    private void Update()
        => _direction = Input.GetAxis(_axisName);

    private void FixedUpdate()
        => Moved?.Invoke(_direction);
}