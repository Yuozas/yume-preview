using SwiftLocator.Services.ServiceLocatorServices;
using System;

public class ExecuteDialogueTypewriterCommand : ICommand
{
    private readonly ICommand _command;

    public ExecuteDialogueTypewriterCommand(string type, TypewriterSettings settings)
    {
        var resolver = ServiceLocator.GetSingleton<DialogueResolver>();
        var dialogue = resolver.Resolve(type);
        var typewriter = dialogue.Typewriter;

        _command = new ExecuteTypewriterCommand(typewriter, settings);
    }

    public void Execute(Action onFinished = null)
    {
        _command.Execute(onFinished);
    }
}
