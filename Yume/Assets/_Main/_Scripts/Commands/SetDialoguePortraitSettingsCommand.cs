using SwiftLocator.Services.ServiceLocatorServices;
using System;
using UnityEngine;

[Serializable]
public class SetDialoguePortraitSettingsCommand : ICommand
{
    [SerializeField] public string Type;
    [SerializeField] public PortraitSettings Settings;

    public SetDialoguePortraitSettingsCommand(string type = null, PortraitSettings? settings = null)
    {
        Type = type ?? Dialogue.DEFAULT;
        Settings = settings ?? PortraitSettings.Default;
    }

    public void Execute(Action onFinished = null)
    {
        var portrait = ServiceLocator.GetSingleton<DialogueResolver>().Resolve(Type).Portrait;
        var command = new SetPortraitSettingsCommand(portrait, Settings);

        command.Execute(onFinished);
    }
}