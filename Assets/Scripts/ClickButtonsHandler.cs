using UnityEngine;
using System;

public class ClickButtonsHandler : MonoBehaviour
{
    private const int _leftMouseButton = 0;

    public event Action LeftMouseButtonPressed;

    private void Update()
    {
        if(Input.GetMouseButtonDown(_leftMouseButton))
            LeftMouseButtonPressed?.Invoke();
    }
}