using UnityEngine;

public class DialogueBuilder
{
    private Typewriter _typewriter;
    private Portrait _portrait;
    private Name _name;

    public void Restart()
    {
        _typewriter = null;
        _portrait = null;
        _name = null;
    }

    public DialogueBuilder WithTypewriter(MonoBehaviour behaviour = null, DelayedExecutorSettings? settings = null)
    {
        var delayedExecutor = new DelayedExecutor(behaviour, settings);
        var typewriterIterator = new TypewriterIterator();

        _typewriter = new Typewriter(delayedExecutor, typewriterIterator);
        return this;
    }

    public DialogueBuilder WithPortrait(PortraitSettings? settings = null)
    {
        _portrait = new Portrait(settings);
        return this;
    }

    public DialogueBuilder WithName(NameSettings? settings = null)
    {
        _name = new Name(settings);
        return this;
    }

    public Dialogue Build(string type)
    {
        return new Dialogue(type, _typewriter, _portrait, _name);
    }
}
