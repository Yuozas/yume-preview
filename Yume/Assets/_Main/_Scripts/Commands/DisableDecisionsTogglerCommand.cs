using SwiftLocator.Services.ServiceLocatorServices;
using System;

[Serializable]
public class DisableDecisionsTogglerCommand : ICommand
{
    public void Execute(Action onFinished = null)
    {
        new DisableTogglerCommand(ServiceLocator.GetSingleton<Decisions>().Toggler)
            .Execute(onFinished);
    }
}
