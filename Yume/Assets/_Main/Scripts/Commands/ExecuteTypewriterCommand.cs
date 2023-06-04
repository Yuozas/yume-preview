using System;
using UnityEngine;

[Serializable]
public class ExecuteTypewriterCommand : ICommand
{
    [SerializeReference] private Typewriter _typewriter;
    [SerializeField] private TypewriterSettings _settings;

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
