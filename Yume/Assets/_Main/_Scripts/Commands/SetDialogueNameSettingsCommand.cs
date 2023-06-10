using SwiftLocator.Services.ServiceLocatorServices;
using System;
using UnityEngine;

[Serializable]
public class SetDialogueNameSettingsCommand : ICommand
{
    [SerializeField] public string Type;
    [SerializeField] public NameSettings Settings;

    public SetDialogueNameSettingsCommand(string type = null, NameSettings? settings = null)
    {
        Type = type ?? Dialogue.DEFAULT;
        Settings = settings ?? NameSettings.Default;
    }

    public void Execute(Action onFinished = null)
    {
        var name = ServiceLocator.GetSingleton<DialogueResolver>().Resolve(Type).Name;
        var command = new SetNameSettingsCommand(name, Settings);

        command.Execute(onFinished);
    }
}
