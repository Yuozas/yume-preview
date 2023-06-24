using SwiftLocator.Services.ServiceLocatorServices;
using System;

[Serializable]
public class DisableDecisionsTogglerCommand : ICommand
{
    public void Execute(Action onFinished = null)
    {
        var toggler = ServiceLocator.GetSingleton<Decisions>().Toggler;
        var command = new DisableTogglerCommand(toggler);

        command.Execute(onFinished);
    }
}
