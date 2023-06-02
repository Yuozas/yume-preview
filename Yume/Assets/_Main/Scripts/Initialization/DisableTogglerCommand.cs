using System;

public class DisableTogglerCommand : ICommand
{
    private readonly IToggler _toggle;

    public DisableTogglerCommand(IToggler toggle)
    {
        _toggle = toggle;
    }
    public void Execute(Action onFinished = null)
    {
        _toggle.Disable();
        onFinished?.Invoke();
    }
}
