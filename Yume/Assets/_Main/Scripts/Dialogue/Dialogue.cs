using System;
using System.Collections.Generic;

public class Dialogue : IToggleProvider
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
