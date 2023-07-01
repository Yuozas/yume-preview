using System.Collections.Generic;
using System;

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
    public SliderGameStage Current => _stages[_index];
    public float CursorPosition { get; private set; }

    private readonly SliderGameStage[] _stages;
    private int _index;
    private List<INode> _winNodes;
    private List<INode> _loseNodes;
    private readonly LoopPercentage _loopPercentage;


    public SliderGame()
    {
        Toggler = new Toggler();

        _stages = new SliderGameStage[3]
        {
            new SliderGameStage(0.3f),
            new SliderGameStage(0.2f),
            new SliderGameStage(0.1f)
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
        Current.Execute(Increment, InvokeLose, CursorPosition);
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
        _loopPercentage.Stop();
        foreach (var node in _loseNodes)
            node.Execute();
    }

    private void SetIndex(int index)
    {
        _index = index;
        OnPercentageUpdated?.Invoke();
    }

    private void Increment()
    {
        var index = _index + 1;
        if(index >= _stages.Length)
        {
            InvokeWin();
            return;
        }

        SetIndex(index);
    }

    private void Set(float percentage)
    {
        UnityEngine.Debug.Log("Set :: " + percentage);
        CursorPosition = percentage;
        OnPercentageUpdated?.Invoke();
    }
}