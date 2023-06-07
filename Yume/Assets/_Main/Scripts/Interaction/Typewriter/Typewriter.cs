using System;
using UnityEngine;

[Serializable]
public class Typewriter
{
    [SerializeReference] private DelayedExecutor _executor;
    [SerializeReference] private TypewriterIterator _builder;

    public event Action<string> OnUpdated;
    private Action _onFinished;

    private const bool IGNORE_SPACE = true;

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

        var @default = settings ?? TypewriterSettings.Default;
        var sentence = @default.Sentence.RemoveNewLinesAndAddSpace();

        _builder.Set(sentence, IGNORE_SPACE);

        var cycles = IGNORE_SPACE ? sentence.Length - sentence.GetWhiteSpaceCount() : sentence.Length;
        var executorSettings = new DelayedExecutorSettings(cycles, @default.Rate);

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
