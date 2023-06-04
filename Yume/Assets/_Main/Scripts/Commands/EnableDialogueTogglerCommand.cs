using SwiftLocator.Services.ServiceLocatorServices;
using System;
using UnityEngine;

[Serializable]
public class EnableDialogueTogglerCommand : ICommand
{
    [SerializeField] public string Type;

    public EnableDialogueTogglerCommand(string type = null)
    {
        Type = type ?? Dialogue.DEFAULT;
    }

    public void Execute(Action onFinished = null)
    {
        var toggler = ServiceLocator.GetSingleton<DialogueResolver>().Resolve(Type).Toggler;
        var command = new EnableTogglerCommand(toggler);

        command.Execute(onFinished);
    }
}