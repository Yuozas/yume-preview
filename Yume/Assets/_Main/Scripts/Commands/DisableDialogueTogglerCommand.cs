using SwiftLocator.Services.ServiceLocatorServices;
using System;

public class DisableDialogueTogglerCommand : ICommand
{
    private readonly string _type;

    public DisableDialogueTogglerCommand(string type)
    {
        _type = type;
    }

    public void Execute(Action onFinished = null)
    {
        var toggler = ServiceLocator.GetSingleton<DialogueResolver>().Resolve(_type).Toggler;
        var command = new DisableTogglerCommand(toggler);

        command.Execute(onFinished);
    }
}
