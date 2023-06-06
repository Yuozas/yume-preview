using System;
using UnityEngine;

[Serializable]
public class EnableTogglerCommand : ICommand
{
    [SerializeReference] private readonly IToggler _toggle;

    public EnableTogglerCommand(IToggler toggle)
    {
        _toggle = toggle;
    }
    public void Execute(Action onFinished = null)
    {
        _toggle.Enable();
        onFinished?.Invoke();
    }
}
