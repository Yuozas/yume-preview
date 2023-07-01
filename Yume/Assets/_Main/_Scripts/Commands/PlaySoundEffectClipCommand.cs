using SwiftLocator.Services.ServiceLocatorServices;
using System;
using UnityEngine;

[Serializable]
public class PlaySoundEffectClipCommand : ICommand
{
    [SerializeField] public SoundEffectClipSettings Settings;

    public PlaySoundEffectClipCommand(SoundEffectClipSettings? settings = null)
    {
        Settings = settings ?? SoundEffectClipSettings.Default;
    }

    public void Execute(Action onFinished = null)
    {
        ServiceLocator.GetSingleton<SoundEffectAudioSource>().Play(Settings);
        onFinished?.Invoke();
    }
}