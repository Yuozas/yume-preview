using SwiftLocator.Services.ServiceLocatorServices;
using System;
using UnityEngine;

[Serializable]
public class ExecuteDialogueTypewriterCommand : ICommand
{
    [SerializeField] public string Type;
    [SerializeField] public TypewriterSettings Settings;

    public ExecuteDialogueTypewriterCommand(string type = null, TypewriterSettings? settings = null)
    {
        Type = type ?? Dialogue.DEFAULT;
        Settings = settings ?? TypewriterSettings.DEFAULT;
    }

    public void Execute(Action onFinished = null)
    {
        var typewriter = ServiceLocator.GetSingleton<DialogueResolver>().Resolve(Type).Typewriter;
        var command = new ExecuteTypewriterCommand(typewriter, Settings);

        command.Execute(onFinished);
    }
}
