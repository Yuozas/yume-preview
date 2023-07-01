using SwiftLocator.Services.ServiceLocatorServices;
using System;

[Serializable]
public class EnableSliderGameTogglerCommand : ICommand
{
    public void Execute(Action onFinished = null)
    {
        var toggler = ServiceLocator.GetSingleton<SliderGame>().Toggler;
        var command = new EnableTogglerCommand(toggler);

        command.Execute(onFinished);
    }
}