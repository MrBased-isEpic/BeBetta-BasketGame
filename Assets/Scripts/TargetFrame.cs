using System;
using UnityEngine;

public class TargetFrame : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
