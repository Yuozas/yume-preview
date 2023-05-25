using UnityEngine;
using SwiftLocator.Services.ServiceLocatorServices;
using SwiftLocator.Services.ScopedServices;
using System.Collections;
using System.Threading.Tasks;
using System;
using System.Threading;
using System.Text;
using System.Collections.Generic;
using System.Linq;

public static class MonoBehaviourInitialization
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    private static void Initialize()
    {
        
    }
}
public static class Initialization
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    private static void Initialize()
    {
        //Load Initialization Scene.
        //Coroutine Monobehaviour.

        RegisterSingletonServices();
        RegisterTransientServices();
        ServiceLocator.Build();
    }

    private static void RegisterSingletonServices()
    {
        const string COROUTINES = "Coroutines";
        var gameObject = new GameObject(COROUTINES);
        var coroutines = gameObject.AddComponent<Coroutines>();

        var dialogues = new List<DialogueHandler>
        {
            CreateDialogueHandler(DialogueHandler.INSPECTION, true, false, false),
            CreateDialogueHandler(DialogueHandler.CONVERSATION, true, true, true)
        };

        var dialogueManager = new DialogueHandlerResolver(dialogues);
        
        ServiceLocator.SingletonRegistrator.Register(dialogueManager);
    }

    private static DialogueHandler CreateDialogueHandler(string type, bool? useTypewriter, bool? usePortrait, bool? useName)
    {
        var typewriter = useTypewriter != null ? CreateTypewriter() : null;
        var portrait = usePortrait != null ? new PortraitHandler() : null;
        var name = useName != null ? new NameHandler() : null;

        return new DialogueHandler(type, typewriter, portrait, name);
    }

    private static TypewriterHandler CreateTypewriter()
    {
        var delayedExecutor = new DelayedExecutor();

        var typewriterIterator = new TypewriterIterator();
        var typewriterHandler = new TypewriterHandler(delayedExecutor, typewriterIterator);
        return typewriterHandler;
    }

    private static void RegisterTransientServices()
    {
        // Todo register transient services.
        //ServiceLocator.TransientRegistrator;
    }
}
public class Coroutines : MonoBehaviour
{
    public void Initialize()
    {

    }
}

public class DialogueHandlerResolver
{
    private readonly List<DialogueHandler> _dialogues;

    public DialogueHandlerResolver(List<DialogueHandler> dialogues)
    {
        _dialogues = dialogues;
    }

    public DialogueHandler Resolve(string type)
    {
        return _dialogues.First(dialogue => dialogue.Type == type);
    }
}

public class DialogueHandler
{
    public const string CONVERSATION = "Conversation";
    public const string INSPECTION = "Inspection";

    public readonly string Type;

    public readonly TypewriterHandler Typewriter;
    public readonly PortraitHandler Portrait;
    public readonly NameHandler Name;

    public DialogueHandler(string type, TypewriterHandler typewriter = null, PortraitHandler portrait = null, NameHandler name = null)
    {
        Type = type;
        Typewriter = typewriter;
        Portrait = portrait;
        Name = name;
    }
}

public static class String
{
    public const string EMPTY = "Empty";
}


public class NameHandler
{
    public event Action<string> OnUpdated;
    public string Text { get; private set; }

    public NameHandler(string text = String.EMPTY)
    {
        Set(text);
    }

    public void Set(string text)
    {
        Text = text;
        OnUpdated?.Invoke(text);
    }
}

public class PortraitHandler
{
    public event Action<Sprite> OnUpdated;
    public Sprite Sprite { get; private set; }
    public PortraitHandler(Sprite sprite = null)
    {
        Set(sprite);
    }

    public void Set(Sprite sprite)
    {
        Sprite = sprite;
        OnUpdated?.Invoke(sprite);
    }
}

public class TypewriterHandler
{
    public event Action<string> OnUpdated;

    private readonly DelayedExecutor _executor;
    private readonly TypewriterIterator _builder;

    public TypewriterHandler(DelayedExecutor executor, TypewriterIterator builder)
    {
        _builder = builder;
        _executor = executor;
        _executor.Updated += Set;
    }
    ~TypewriterHandler()
    {
        _executor.Updated -= Set;
    }

    public void Execute(TypewriterSettings settings, Action onFinished = null)
    {
        _builder.Set(settings.Sentence);

        var executorSettings = new DelayedExecutorSettings(settings.Sentence.Length, settings.Rate);
        _executor.UpdateSettings(executorSettings);
        _executor.Begin(onFinished);
    }

    private void Set()
    {
        _builder.Next();
        OnUpdated?.Invoke(_builder.Current);
    }
}

public struct TypewriterSettings
{
    public static readonly TypewriterSettings DEFAULT = new(SENTENCE, 1);
    private static readonly string SENTENCE = "Sentence";

    public string Sentence;
    public float Rate;

    public TypewriterSettings(string text, float rate)
    {
        Sentence = text;
        Rate = rate;
    }
}

public class TypewriterIterator
{
    public string Current => _builder.ToString();

    private string _sentence;
    private readonly StringBuilder _builder;

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


    public void Begin(Action onFinished = null)
    {
        Stop();

        _ienumerator = Execute(onFinished);
        _behaviour.StartCoroutine(_ienumerator);
    }

    protected abstract IEnumerator Execute(Action finished);
}

public class DelayedExecutor : CoroutineHandler
{
    public event Action Updated;

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
            Updated?.Invoke();
            yield return wait;
        }

        onFinished?.Invoke();
    }
}

public struct DelayedExecutorSettings
{
    public static readonly DelayedExecutorSettings DEFAULT = new(0, 1);

    public int Cycles;
    public float Rate;

    public DelayedExecutorSettings(int cycles, float rate)
    {
        Cycles = cycles;
        Rate = rate;
    }
}