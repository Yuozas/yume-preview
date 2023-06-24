using SwiftLocator.Services.ServiceLocatorServices;
using System;
using UnityEngine;

[Serializable]
public class DisableDialogueTogglerCommand : ICommand
{
    [SerializeField] public string Type;

    public DisableDialogueTogglerCommand(string type = null)
    {
        Type = type ?? Dialogue.DEFAULT;
    }

    public void Execute(Action onFinished = null)
    {
        var toggler = ServiceLocator.GetSingleton<DialogueResolver>().Resolve(Type).Toggler;
        var command = new DisableTogglerCommand(toggler);

        command.Execute(onFinished);
    }
}