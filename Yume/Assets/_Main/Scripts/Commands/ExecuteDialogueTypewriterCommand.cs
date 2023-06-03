using SwiftLocator.Services.ServiceLocatorServices;
using System;

public class ExecuteDialogueTypewriterCommand : ICommand
{
    private readonly string _type;
    private readonly TypewriterSettings _settings;

    public ExecuteDialogueTypewriterCommand(string type, TypewriterSettings settings)
    {
        _type = type;
        _settings = settings;
    }

    public void Execute(Action onFinished = null)
    {
        var typewriter = ServiceLocator.GetSingleton<DialogueResolver>().Resolve(_type).Typewriter;
        var command = new ExecuteTypewriterCommand(typewriter, _settings);

        command.Execute(onFinished);
    }
}
