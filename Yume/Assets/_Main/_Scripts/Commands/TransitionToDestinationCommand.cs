using SwiftLocator.Services.ServiceLocatorServices;
using System;
using UnityEngine;

[Serializable]
public class TransitionToDestinationCommand : ICommand
{
    [SerializeField] public TransitionDestinationScriptableObject DestinationScriptableObject;

    public TransitionToDestinationCommand(TransitionDestinationScriptableObject destinationScriptableObject = null)
    {
        DestinationScriptableObject = destinationScriptableObject;
    }

    public void Execute(Action onFinished = null)
    {
        ServiceLocator.GetSingleton<Transitioner>().Transition(DestinationScriptableObject, onFinished);
    }
}