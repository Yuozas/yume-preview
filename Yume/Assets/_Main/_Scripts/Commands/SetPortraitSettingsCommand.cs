using System;
using UnityEngine;

[Serializable]
public class SetPortraitSettingsCommand : ICommand
{
    [SerializeReference] private Portrait _portrait;
    [SerializeField] private PortraitSettings _settings;

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
