using SwiftLocator.Services.ServiceLocatorServices;
using System;
using UnityEngine;

[Serializable]
public class PlaySfxClipCommand : ICommand
{
    [SerializeField] public SfxClipSettings Settings;

    public PlaySfxClipCommand(SfxClipSettings? settings = null)
    {
        Settings = settings ?? SfxClipSettings.Default;
    }

    public void Execute(Action onFinished = null)
    {
        ServiceLocator.GetSingleton<Sfx>().Play(Settings);
        onFinished?.Invoke();
    }
}