using System;

public class DialogueHandler
{
    public const string CONVERSATION = "Conversation";
    public const string INSPECTION = "Inspection";

    public readonly string Type;

    public readonly TypewriterHandler Typewriter;
    public readonly PortraitHandler Portrait;
    public readonly NameHandler Name;

    public bool Active { get; private set; }
    public event Action<bool> OnActiveBool;
    public event Action<bool, DialogueHandler> OnActiveObject;

    public DialogueHandler(string type, TypewriterHandler typewriter = null, PortraitHandler portrait = null, NameHandler name = null)
    {
        Type = type;
        Typewriter = typewriter;
        Portrait = portrait;
        Name = name;
    }

    public void SetActive(bool active)
    {
        Active = active;
        if(!Active)
            Typewriter.Stop();

        OnActiveBool?.Invoke(active);
        OnActiveObject?.Invoke(active, this);
    }
}