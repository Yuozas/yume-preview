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
            INode.PLAY_SOUND_EFFECT => BuildPlaySoundEffect(),
            INode.PORTRAIT => BuildPortrait(),
            INode.NAME => BuildName(),
            INode.TYPEWRITER => BuildTypewriter(),
            INode.ENABLE_DECISIONS => BuildEnableDecision(),
            INode.DISABLE_DECISIONS => BuildDisableDecision(),
            INode.SET_DECISION_CHOICES => BuildSetChoices(),
            INode.TRANSITION_TO_DESTINATION => BuildTransitionToDestination(),
            INode.ENABLE_SLIDER_GAME => BuildEnableSlider(),
            INode.DISABLE_SLIDER_GAME => BuildDisableSlider(),
            INode.PLAY_SLIDER_GAME => BuildPlaySlider(),
            INode.WAIT => BuildWait(),
            INode.INVOKE_SCRIPTABLE_OBJECT_EVENT => BuildScriptableObjectEvent(),
            INode.ADD_QUEST => BuildAddQuest(),
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

    public INode BuildDisableDecision()
    {
        var command = new DisableDecisionsTogglerCommand();
        return new CompositeNode(INode.DISABLE_DECISIONS, false, command);
    }

    public INode BuildEnableDecision()
    {
        var command = new EnableDecisionsTogglerCommand();
        return new CompositeNode(INode.ENABLE_DECISIONS, false, command);
    }

    public INode BuildDisableSlider()
    {
        var command = new DisableSliderGameTogglerCommand();
        return new CompositeNode(INode.DISABLE_SLIDER_GAME, false, command);
    }

    public INode BuildPlaySlider()
    {
        return new SliderGameNode(INode.PLAY_SLIDER_GAME);
    }

    public INode BuildWait()
    {
        var command = new WaitCommand();
        return new CompositeNode(INode.WAIT, true, command);
    }

    public INode BuildScriptableObjectEvent()
    {
        var command = new InvokeScriptableObjectEventCommand();
        return new CompositeNode(INode.INVOKE_SCRIPTABLE_OBJECT_EVENT, false, command);
    }

    public INode BuildAddQuest()
    {
        var command = new AddQuestCommand();
        return new CompositeNode(INode.ADD_QUEST, false, command);
    }

    public INode BuildEnableSlider()
    {
        var command = new EnableSliderGameTogglerCommand();
        return new CompositeNode(INode.ENABLE_SLIDER_GAME, false, command);
    }

    public INode BuildSetChoices()
    {
        return new ChoicesNode(INode.SET_DECISION_CHOICES);
    }

    public INode BuildMusic()
    {
        var command = new PlayMusicClipCommand();
        return new CompositeNode(INode.MUSIC, false, command);
    }

    public INode BuildPlaySoundEffect()
    {
        var command = new PlaySoundEffectClipCommand();
        return new CompositeNode(INode.PLAY_SOUND_EFFECT, false, command);
    }

    public INode BuildDisable()
    {
        var command = new DisableDialogueTogglerCommand(_type);
        return new CompositeNode(INode.DISABLE, false, command);
    }

    public INode BuildTransitionToDestination()
    {
        var command = new TransitionToDestinationCommand();
        return new CompositeNode(INode.TRANSITION_TO_DESTINATION, true, command);
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