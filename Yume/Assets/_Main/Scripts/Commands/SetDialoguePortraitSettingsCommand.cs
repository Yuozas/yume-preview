using SwiftLocator.Services.ServiceLocatorServices;
using System;

public class SetDialoguePortraitSettingsCommand : ICommand
{
    private readonly string _type;
    private readonly PortraitSettings _settings;

    public SetDialoguePortraitSettingsCommand(string type, PortraitSettings settings)
    {
        _type = type;
        _settings = settings;
    }

    public void Execute(Action onFinished = null)
    {
        var portrait = ServiceLocator.GetSingleton<DialogueResolver>().Resolve(_type).Portrait;
        var command = new SetPortraitSettingsCommand(portrait, _settings);

        command.Execute(onFinished);
    }
}