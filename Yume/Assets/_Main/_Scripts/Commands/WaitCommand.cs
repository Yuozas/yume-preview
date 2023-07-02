using System;
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
        var settings = new DelayedExecutorSettings(1, 0.05f);
        var executor = new DelayedExecutor(settings: settings);

        executor.Begin(() => onFinished?.Invoke());
    }
}