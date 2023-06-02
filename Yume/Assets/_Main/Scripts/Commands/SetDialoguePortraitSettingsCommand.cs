using SwiftLocator.Services.ServiceLocatorServices;
using System;

public class SetDialoguePortraitSettingsCommand : ICommand
{
    private readonly ICommand _command;

    public SetDialoguePortraitSettingsCommand(string type, PortraitSettings settings)
    {
        var resolver = ServiceLocator.GetSingleton<DialogueResolver>();
        var dialogue = resolver.Resolve(type);
        var portrait = dialogue.Portrait;

        _command = new SetPortraitSettingsCommand(portrait, settings);
    }

    public void Execute(Action onFinished = null)
    {
        _command.Execute(onFinished);
    }
}