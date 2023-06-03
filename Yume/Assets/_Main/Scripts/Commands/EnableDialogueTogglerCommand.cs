using SwiftLocator.Services.ServiceLocatorServices;
using System;

public class EnableDialogueTogglerCommand
{
    private readonly string _type;

    public EnableDialogueTogglerCommand(string type)
    {
        _type = type;
    }

    public void Execute(Action onFinished = null)
    {
        var toggler = ServiceLocator.GetSingleton<DialogueResolver>().Resolve(_type).Toggler;
        var command = new EnableTogglerCommand(toggler);

        command.Execute(onFinished);
    }
}