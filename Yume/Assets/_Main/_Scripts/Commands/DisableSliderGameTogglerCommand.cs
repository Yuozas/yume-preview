using SwiftLocator.Services.ServiceLocatorServices;
using System;

[Serializable]
public class DisableSliderGameTogglerCommand : ICommand
{
    public void Execute(Action onFinished = null)
    {
        var toggler = ServiceLocator.GetSingleton<SliderGame>().Toggler;
        var command = new DisableTogglerCommand(toggler);

        command.Execute(onFinished);
    }
}