using System;
using UnityEngine;

[Serializable]
public class Typewriter
{
    public event Action<string> OnUpdated;

    [SerializeReference] private DelayedExecutor _executor;
    [SerializeReference] private TypewriterIterator _builder;

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
        var @default = settings ?? TypewriterSettings.DEFAULT;
        _builder.Set(@default.Sentence);

        var executorSettings = new DelayedExecutorSettings(@default.Sentence.Length, @default.Rate);
        _executor.UpdateSettings(executorSettings);
        _executor.Begin(onFinished);
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
