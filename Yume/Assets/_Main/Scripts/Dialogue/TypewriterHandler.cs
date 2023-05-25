using System;

public class TypewriterHandler
{
    public event Action<string> OnUpdated;

    private readonly DelayedExecutor _executor;
    private readonly TypewriterIterator _builder;

    public TypewriterHandler(DelayedExecutor executor, TypewriterIterator builder)
    {
        _builder = builder;
        _executor = executor;
        _executor.OnUpdated += Set;
    }
    ~TypewriterHandler()
    {
        _executor.OnUpdated -= Set;
    }

    public void Execute(TypewriterSettings settings, Action onFinished = null)
    {
        _builder.Set(settings.Sentence);

        var executorSettings = new DelayedExecutorSettings(settings.Sentence.Length, settings.Rate);
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
