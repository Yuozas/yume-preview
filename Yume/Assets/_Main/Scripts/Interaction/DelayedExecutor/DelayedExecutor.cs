using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class DelayedExecutor : CoroutineHandler
{
    public event Action OnUpdated;

    [SerializeField] private DelayedExecutorSettings _settings;

    public DelayedExecutor(MonoBehaviour behaviour = null, DelayedExecutorSettings? settings = null) : base(behaviour)
    {
        var @default = settings ?? DelayedExecutorSettings.Default;
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

        Stop();
        onFinished?.Invoke();
    }
}