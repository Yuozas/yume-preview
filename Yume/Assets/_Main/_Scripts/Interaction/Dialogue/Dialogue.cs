using System.Collections.Generic;
using System;
using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;

public class Dialogue : ITogglerProvider
{
    public const string DEFAULT = CONVERSATION;
    public static readonly List<string> Types = new()
    {
        CONVERSATION,
        INSPECTION
    };

    public const string CONVERSATION = "Conversation";
    public const string INSPECTION = "Inspection";

    public readonly string Type;

    public readonly Typewriter Typewriter;
    public readonly Portrait Portrait;
    public readonly Name Name;
    public IToggler Toggler { get; private set; }

    public Dialogue(string type, Typewriter typewriter = null, Portrait portrait = null, Name name = null)
    {
        Type = type;
        Typewriter = typewriter;
        Portrait = portrait;
        Name = name;

        Toggler = new Toggler();
    }
}

public class SliderGame : ITogglerProvider
{
    public IToggler Toggler { get; private set; }
    public event Action OnPercentageUpdated;

    public float CursorPosition { get; private set; }

    public int Index { get; private set; }
    public int Total => _stages.Length;
    public SliderGameStage Current => _stages[Index];

    private List<INode> _winNodes;
    private List<INode> _loseNodes;
    private readonly SliderGameStage[] _stages;
    private readonly LoopPercentage _loopPercentage;
    private readonly SoundEffectAudioSource _soundEffect;
    private readonly DelayedExecutor _executor;

    public SliderGame()
    {
        Toggler = new Toggler();

        var settings = new DelayedExecutorSettings(1, 0.75f);
        _executor = new DelayedExecutor(settings: settings);

        _soundEffect = ServiceLocator.GetSingleton<SoundEffectAudioSource>();

        _stages = new SliderGameStage[5]
        {
            new SliderGameStage(0.3f),
            new SliderGameStage(0.2f),
            new SliderGameStage(0.1f),
            new SliderGameStage(0.05f),
            new SliderGameStage(0.5f)
        };

        _loopPercentage = new LoopPercentage();
        _loopPercentage.OnUpdated += Set;
    }

    ~SliderGame()
    {
        _loopPercentage.OnUpdated -= Set;
    }

    public void Execute()
    {
        if (_executor.Running || _executor.Paused)
            return;

        _loopPercentage.Pause();
        Current.Execute(PlaySoundEffectAndIncrement, InvokeLose, CursorPosition);
    }

    public void Begin(List<INode> winNodes, List<INode> loseNodes)
    {
        _winNodes = winNodes;
        _loseNodes = loseNodes;

        SetIndex(0);
        _loopPercentage.Begin(1f);
    }

    private void InvokeWin()
    {
        _loopPercentage.Stop();
        foreach (var node in _winNodes)
            node.Execute();
    }

    private void InvokeLose()
    {
        var clip = Resources.Load<AudioClip>("Incorrect");
        var settings = new SoundEffectClipSettings(clip, 8);
        _soundEffect.Play(settings);

        _loopPercentage.Stop();
        foreach (var node in _loseNodes)
            node.Execute();
    }

    private void SetIndex(int index)
    {
        Index = index;
        OnPercentageUpdated?.Invoke();
    }

    private void PlaySoundEffectAndIncrement()
    {
        var index = Index + 1;

        var clip = Resources.Load<AudioClip>(index < _stages.Length ? "Correct" : "Success");
        var settings = new SoundEffectClipSettings(clip, index < _stages.Length ? 8 : 0.75f);

        _soundEffect.Play(settings);
        _executor.Begin(Increment);
    }

    private void Increment()
    {
        _loopPercentage.Unpause();
        var index = Index + 1;
        if (index >= _stages.Length)
        {
            InvokeWin();
            return;
        }

        SetIndex(index);
    }

    private void Set(float percentage)
    {
        CursorPosition = percentage;
        OnPercentageUpdated?.Invoke();
    }
}