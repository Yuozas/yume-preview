using System;
using UnityEngine;

[Serializable]
public class InvokeScriptableObjectEventCommand : ICommand
{
    [SerializeField] public EventScriptableObject EventScriptableObject;

    public InvokeScriptableObjectEventCommand(EventScriptableObject eventScriptableObject = null)
    {
        EventScriptableObject = eventScriptableObject;
    }

    public void Execute(Action onFinished = null)
    {
        EventScriptableObject.Invoke();
        onFinished?.Invoke();
    }
}