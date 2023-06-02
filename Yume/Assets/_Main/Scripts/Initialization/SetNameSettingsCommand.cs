using System;

public class SetNameSettingsCommand : ICommand
{
    private readonly Name _name;
    private readonly NameSettings _settings;

    public SetNameSettingsCommand(Name name, NameSettings settings)
    {
        _name = name;
        _settings = settings;
    }

    public void Execute(Action onFinished = null)
    {
        _name.Set(_settings);
        onFinished?.Invoke();
    }
}
