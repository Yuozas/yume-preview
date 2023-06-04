using System;
using UnityEngine;

[Serializable]
public class Typewriter
{
    public event Action<string> OnUpdated;

    [SerializeReference] private DelayedExecutor _executor;
    [SerializeReference] private TypewriterIterator _builder;

    private Action _onFinished;

    public Typewriter(DelayedExecutor executor, TypewriterIterator builder)
    {
        _builder = builder;
        _executor = executor;
        _executor.OnUpdated += Set;
    }
    ~Typewriter()
    {
        _executor.OnUpdated -= Set;
    }

    public void Execute(TypewriterSettings? settings = null, Action onFinished = null)
    {
        _onFinished = onFinished;

        var @default = settings ?? TypewriterSettings.DEFAULT;
        _builder.Set(@default.Sentence);

        var executorSettings = new DelayedExecutorSettings(@default.Sentence.Length, @default.Rate);
        _executor.UpdateSettings(executorSettings);
        _executor.Begin();
    }

    public void Continue()
    {
        if (_executor.Running)
        {
            _executor.Stop();
            _builder.Complete();
            OnUpdated?.Invoke(_builder.Current);
            return;
        }

        _onFinished?.Invoke();
    }
    public void Stop()
    {
        _executor.Stop();
    }

    private void Set()
    {
        _builder.Next();
        OnUpdated?.Invoke(_builder.Current);
    }
}
