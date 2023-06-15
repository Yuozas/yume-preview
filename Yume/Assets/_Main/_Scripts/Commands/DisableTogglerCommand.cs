using System;
using UnityEngine;

[Serializable]
public class DisableTogglerCommand : ICommand
{
    [SerializeReference] private readonly IToggler _toggle;

    public DisableTogglerCommand(IToggler toggle)
    {
        _toggle = toggle;
    }

    public void Execute(Action onFinished = null)
    {
        _toggle.Disable();
        onFinished?.Invoke();
    }
}
