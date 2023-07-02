using UnityEngine;
using System.Collections;
using System;
using SwiftLocator.Services.ServiceLocatorServices;

public abstract class CoroutineHandler
{
    private readonly MonoBehaviour _behaviour;
    private IEnumerator _ienumerator;

    public bool Running => _ienumerator is not null;
    public bool Paused { get; private set; }

    public CoroutineHandler(MonoBehaviour behaviour)
    {
        var @default = ServiceLocator.GetSingleton<MonoBehaviour>();
        _behaviour = behaviour is not null ? behaviour : @default;
    }

    public void Stop()
    {
        if (_ienumerator is null)
            return;

        _behaviour.StopCoroutine(_ienumerator);
        _ienumerator = null;
    }

    public void Begin(Action onFinished = null)
    {
        Stop();
        Unpause();

        _ienumerator = Execute(onFinished);
        _behaviour.StartCoroutine(_ienumerator);
    }

    public void Pause()
    {
        Paused = true;
    }

    public void Unpause()
    {
        Paused = false;
    }

    protected abstract IEnumerator Execute(Action onFinished);
}
