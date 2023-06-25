using SwiftLocator.Services.ServiceLocatorServices;
using System;

[Serializable]
public class EnableDecisionsTogglerCommand : ICommand
{
    public void Execute(Action onFinished = null)
    {
        new EnableTogglerCommand(ServiceLocator.GetSingleton<Decisions>().Toggler)
            .Execute(onFinished);
    }
}