using System;

public class SetPortraitSettingsCommand : ICommand
{
    private readonly Portrait _portrait;
    private readonly PortraitSettings _settings;

    public SetPortraitSettingsCommand(Portrait portrait, PortraitSettings settings)
    {
        _portrait = portrait;
        _settings = settings;
    }

    public void Execute(Action onFinished = null)
    {
        _portrait.Set(_settings);
        onFinished?.Invoke();
    }
}
