using System;

public class ExecuteTypewriterCommand : ICommand
{
    private readonly Typewriter _typewriter;
    private readonly TypewriterSettings _settings;

    public ExecuteTypewriterCommand(Typewriter typewriter, TypewriterSettings settings)
    {
        _typewriter = typewriter;
        _settings = settings;
    }

    public void Execute(Action onFinished = null)
    {
        _typewriter.Execute(_settings, onFinished);
    }
}
