using UnityEngine;
using System;

public class TargetPoint : MonoBehaviour
{
    public event Action<TargetPoint> Releasing;
}