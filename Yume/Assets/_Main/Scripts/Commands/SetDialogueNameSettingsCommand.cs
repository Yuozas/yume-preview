using SwiftLocator.Services.ServiceLocatorServices;
using System;

public class SetDialogueNameSettingsCommand : ICommand
{
    private readonly string _type;
    private readonly NameSettings _settings;

    public SetDialogueNameSettingsCommand(string type, NameSettings settings)
    {
        _type = type;
        _settings = settings;
    }

    public void Execute(Action onFinished = null)
    {
        var name = ServiceLocator.GetSingleton<DialogueResolver>().Resolve(_type).Name;
        var command = new SetNameSettingsCommand(name, _settings);

        command.Execute(onFinished);
    }
}
