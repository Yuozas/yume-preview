using SwiftLocator.Services.ServiceLocatorServices;
using System;

[Serializable]
public class EnableDecisionsTogglerCommand : ICommand
{
    public void Execute(Action onFinished = null)
    {
        var toggler = ServiceLocator.GetSingleton<Decisions>().Toggler;
        var command = new EnableTogglerCommand(toggler);

        command.Execute(onFinished);
    }
}