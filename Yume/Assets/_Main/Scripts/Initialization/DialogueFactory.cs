public class DialogueFactory
{
    private readonly DialogueBuilder _builder;

    public DialogueFactory()
    {
        _builder = new DialogueBuilder();
    }
    public Dialogue BuildConversation()
    {
        _builder.Restart();

        return _builder
            .WithName()
            .WithPortrait()
            .WithTypewriter()
            .Build(Dialogue.CONVERSATION);
    }

    public Dialogue BuildInspection()
    {
        _builder.Restart();

        return _builder
            .WithTypewriter()
            .Build(Dialogue.INSPECTION);
    }
}