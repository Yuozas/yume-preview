﻿using UnityEngine;
using System.Collections;
using System;

public class DelayedExecutor : CoroutineHandler
{
    public event Action OnUpdated;

    private DelayedExecutorSettings _settings;

    public DelayedExecutor(MonoBehaviour behaviour = null, DelayedExecutorSettings? settings = null) : base(behaviour)
    {
        var @default = settings ?? DelayedExecutorSettings.DEFAULT;
        UpdateSettings(@default);
    }

    public void UpdateSettings(DelayedExecutorSettings settings)
    {
        _settings = settings;
    }

    protected override IEnumerator Execute(Action onFinished = null)
    {
        var wait = new WaitForSeconds(_settings.Rate);
        for (int i = 0; i < _settings.Cycles; i++)
        {
            OnUpdated?.Invoke();
            yield return wait;
        }

        onFinished?.Invoke();
    }
}