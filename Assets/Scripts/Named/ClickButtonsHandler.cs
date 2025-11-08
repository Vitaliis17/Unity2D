using UnityEngine;
using System;

public class ClickButtonsHandler : MonoBehaviour
{
    private const int _leftMouseButton = 0;
    private const KeyCode _firstSideMouseButton = KeyCode.Mouse3;

    public event Action LeftMouseButtonPressed;
    public event Action FirstSideMouseButtonPressed;

    private void Update()
    {
        if(Input.GetMouseButtonDown(_leftMouseButton))
            LeftMouseButtonPressed?.Invoke();

        if(Input.GetKey(_firstSideMouseButton))
            FirstSideMouseButtonPressed?.Invoke();
    }
}