using UnityEngine;
using System.Collections;
using System;
using SwiftLocator.Services.ServiceLocatorServices;

public abstract class CoroutineHandler
{
    private readonly MonoBehaviour _behaviour;
    private IEnumerator _ienumerator;

    public bool Running => _ienumerator != null;

    public CoroutineHandler(MonoBehaviour behaviour)
    {
        var @default = ServiceLocator.GetSingleton<MonoBehaviour>();
        _behaviour = behaviour != null ? behaviour : @default;
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

        _ienumerator = Execute(onFinished);
        _behaviour.StartCoroutine(_ienumerator);
    }

    protected abstract IEnumerator Execute(Action finished);
}
