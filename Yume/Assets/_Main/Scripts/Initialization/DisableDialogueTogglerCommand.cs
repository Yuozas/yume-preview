using SwiftLocator.Services.ServiceLocatorServices;
using System;

public class DisableDialogueTogglerCommand : ICommand
{
    private readonly ICommand _command;

    public DisableDialogueTogglerCommand(string type)
    {
        var resolver = ServiceLocator.GetSingleton<DialogueResolver>();
        var dialogue = resolver.Resolve(type);
        var toggler = dialogue.Toggler;

        _command = new DisableTogglerCommand(toggler);
    }

    public void Execute(Action onFinished = null)
    {
        _command.Execute(onFinished);
    }
}
