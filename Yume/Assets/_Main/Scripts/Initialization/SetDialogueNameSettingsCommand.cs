using SwiftLocator.Services.ServiceLocatorServices;
using System;

public class SetDialogueNameSettingsCommand : ICommand
{
    private readonly ICommand _command;

    public SetDialogueNameSettingsCommand(string type, NameSettings settings)
    {
        var resolver = ServiceLocator.GetSingleton<DialogueResolver>();
        var dialogue = resolver.Resolve(type);
        var name = dialogue.Name;

        _command = new SetNameSettingsCommand(name, settings);
    }

    public void Execute(Action onFinished = null)
    {
        _command.Execute(onFinished);
    }
}
