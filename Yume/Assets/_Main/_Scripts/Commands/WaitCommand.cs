﻿using System;
using UnityEngine;

[Serializable]
public class WaitCommand : ICommand
{
    [SerializeField] public float Duration;

    public WaitCommand(float duration = 1)
    {
        Duration = duration;
    }

    public void Execute(Action onFinished = null)
    {
        var executor = new DelayedExecutor(settings: DelayedExecutorSettings.Default);
        executor.Begin(() => onFinished?.Invoke());
    }
}