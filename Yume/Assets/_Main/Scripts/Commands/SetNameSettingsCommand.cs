using System;
using UnityEngine;

[Serializable]
public class SetNameSettingsCommand : ICommand
{
    [SerializeReference] private Name _name;
    [SerializeField] private NameSettings _settings;

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
