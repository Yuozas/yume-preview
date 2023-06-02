using System;

public class EnableTogglerCommand : ICommand
{
    private readonly IToggler _toggle;

    public EnableTogglerCommand(IToggler toggle)
    {
        _toggle = toggle;
    }
    public void Execute(Action onFinished = null)
    {
        _toggle.Enable();
        onFinished?.Invoke();
    }
}
