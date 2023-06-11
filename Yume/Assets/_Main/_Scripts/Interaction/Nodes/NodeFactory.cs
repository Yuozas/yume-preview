public class NodeFactory
{
    private readonly string _type;
    public NodeFactory(string type)
    {
        _type = type;
    }

    public INode Build(string type)
    {
        return type switch
        {
            INode.ENTRY => BuildEntry(),
            INode.EXIT => BuildExit(),
            INode.ENABLE => BuildEnable(),
            INode.DISABLE => BuildDisable(),
            INode.MUSIC => BuildMusic(),
            INode.PORTRAIT => BuildPortrait(),
            INode.NAME => BuildName(),
            INode.TYPEWRITER => BuildTypewriter(),
            _ => null,
        };
    }
    public INode BuildEntry()
    {
        return new CompositeNode(INode.ENTRY, false);
    }
    public INode BuildExit()
    {
        return new CompositeNode(INode.EXIT, false);
    }

    public INode BuildTypewriter()
    {
        var command = new ExecuteDialogueTypewriterCommand(_type);
        return new CompositeNode(INode.TYPEWRITER, true, command);
    }

    public INode BuildEnable()
    {
        var command = new EnableDialogueTogglerCommand(_type);
        return new CompositeNode(INode.ENABLE, false, command);
    }

    public INode BuildMusic()
    {
        var command = new PlayMusicClipCommand();
        return new CompositeNode(INode.MUSIC, false, command);
    }

    public INode BuildDisable()
    {
        var command = new DisableDialogueTogglerCommand(_type);
        return new CompositeNode(INode.DISABLE, false, command);
    }

    public INode BuildPortrait()
    {
        var command = new SetDialoguePortraitSettingsCommand(_type);
        return new CompositeNode(INode.PORTRAIT, false, command);
    }

    public INode BuildName()
    {
        var command = new SetDialogueNameSettingsCommand(_type);
        return new CompositeNode(INode.NAME, false, command);
    }
}