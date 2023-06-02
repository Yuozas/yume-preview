using SwiftLocator.Services.ServiceLocatorServices;
using System;

public class EnableDialogueTogglerCommand
{
    private readonly ICommand _command;

    public EnableDialogueTogglerCommand(string type)
    {
        var resolver = ServiceLocator.GetSingleton<DialogueResolver>();
        var dialogue = resolver.Resolve(type);
        var toggler = dialogue.Toggler;

        _command = new EnableTogglerCommand(toggler);
    }

    public void Execute(Action onFinished = null)
    {
        _command.Execute(onFinished);
    }
}