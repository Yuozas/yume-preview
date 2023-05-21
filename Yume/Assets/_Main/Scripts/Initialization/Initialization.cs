using UnityEngine;
using SwiftLocator.Services.ServiceLocatorServices;
using SwiftLocator.Services.ScopedServices;
using System.Collections;
using System.Threading.Tasks;
using System;
using System.Threading;
using System.Text;

public static class Initialization
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    private static void Initialize()
    {
        RegisterSingletonServices();
        RegisterTransientServices();
        ServiceLocator.Build();
    }

    private static void RegisterSingletonServices()
    {
        // Todo register scoped services.
        //ServiceLocator.SingletonRegistrator;
    }

    private static void RegisterTransientServices()
    {
        // Todo register transient services.
        //ServiceLocator.TransientRegistrator;
    }
}

public abstract class Dialogue
{
    public bool Opened { get; private set; }
    public readonly Typewriter Typewriter;

    public Dialogue(Typewriter typewriter)
    {
        Typewriter = typewriter;
    }

    private void Set(bool opened)
    {
        Opened = opened;
    }

    public void Open()
    {
        Set(true);
    }

    public void Close()
    {
        Set(false);
    }

    public void Execute(string sentence, float speed)
    {
        Open();
        Typewriter.Execute(sentence, speed);
    }
}

public class Conversation : Dialogue
{
    public readonly Sprite Icon;
    public readonly string Name;
    public Conversation(Typewriter typewriter, Sprite icon, string name) : base(typewriter)
    {
        Icon = icon;
        Name = name;
    }
}

public class Inspection : Dialogue
{
    public Inspection(Typewriter typewriter) : base(typewriter)
    {

    }
}

public class Typewriter
{
    public event Action<string> OnUpdated;

    private readonly DelayedExecutor _executor;
    private readonly TypewriterIterator _builder;

    public Typewriter(DelayedExecutor executor, TypewriterIterator builder)
    {
        _builder = builder;
        _executor = executor;
        _executor.Updated += Set;
    }
    ~Typewriter()
    {
        _executor.Updated -= Set;
    }

    public void Execute(string sentence, float speed)
    {
        _builder.Set(sentence);

        var settings = new DelayedExecutorSettings(sentence.Length, speed);
        _executor.UpdateSettings(settings);
        _executor.Begin();
    }

    private void Set()
    {
        _builder.Next();
        OnUpdated?.Invoke(_builder.Current);
    }
}

public class TypewriterIterator
{
    public string Current => _builder.ToString();

    private string _sentence;
    private StringBuilder _builder;

    public TypewriterIterator()
    {
        _builder = new();
    }
    public void Set(string sentence)
    {
        _builder.Clear();
        _sentence = sentence;
    }

    public void Next()
    {
        var character = _sentence[_builder.Length];
        _builder.Append(character);
    }
}

public abstract class CoroutineHandler
{
    private readonly MonoBehaviour _behaviour;
    private IEnumerator _ienumerator;

    public CoroutineHandler(MonoBehaviour behaviour)
    {
        _behaviour = behaviour;
    }

    public void Stop()
    {
        if (_ienumerator is null)
            return;

        _behaviour.StopCoroutine(_ienumerator);
        _ienumerator = null;
    }


    public void Begin(Action finished = null)
    {
        Stop();

        _ienumerator = Execute(finished);
        _behaviour.StartCoroutine(_ienumerator);
    }

    protected abstract IEnumerator Execute(Action finished);
}

public class DelayedExecutor : CoroutineHandler
{
    public event Action Updated;

    private DelayedExecutorSettings _settings;

    public DelayedExecutor(MonoBehaviour behaviour, DelayedExecutorSettings? settings = null) : base(behaviour)
    {
        var @default = settings ?? DelayedExecutorSettings.DEFAULT;
        UpdateSettings(@default);
    }

    public void UpdateSettings(DelayedExecutorSettings settings)
    {
        _settings = settings;
    }

    protected override IEnumerator Execute(Action finished = null)
    {
        var wait = new WaitForSeconds(_settings.Seconds);
        for (int i = 0; i < _settings.Cycles; i++)
        {
            Updated?.Invoke();
            yield return wait;
        }

        finished?.Invoke();
    }
}

public struct DelayedExecutorSettings
{
    public static readonly DelayedExecutorSettings DEFAULT = new(0, 1);

    public int Cycles;
    public float Seconds;

    public DelayedExecutorSettings(int cycles, float seconds)
    {
        Cycles = cycles;
        Seconds = seconds;
    }
}