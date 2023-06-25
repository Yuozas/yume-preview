using SwiftLocator.Services.ServiceLocatorServices;
using System;
using UnityEngine;

[Serializable]
public class SetChoicesCommand : ICommand
{
    [SerializeField] private Choice[] _choices;

    public SetChoicesCommand(params Choice[] choices)
    {
        _choices = choices;
    }

    public void Execute(Action onFinished = null)
    {
        ServiceLocator.GetSingleton<Decisions>().Choices.Update(_choices);
    }
}