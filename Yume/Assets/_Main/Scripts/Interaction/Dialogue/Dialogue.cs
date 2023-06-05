using System;
using System.Collections.Generic;

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

    public static event Action Enabled;
    public event Action Disabled;

    public Dialogue(string type, Typewriter typewriter = null, Portrait portrait = null, Name name = null)
    {
        Type = type;
        Typewriter = typewriter;
        Portrait = portrait;
        Name = name;

        Toggler = new Toggler();

        Toggler.OnEnable += InvokeEnabled;
        Toggler.OnDisable += InvokeDisabled;
    }

    ~Dialogue()
    {
        Toggler.OnEnable -= InvokeEnabled;
        Toggler.OnDisable -= InvokeDisabled;
    }

    private void InvokeEnabled()
    {
        Enabled?.Invoke();
    }

    private void InvokeDisabled()
    {
        Disabled?.Invoke();
    }
}
